using System;
using _Project.Scripts.Environment;
using _Project.Scripts.Interface;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class PlayerInteractions : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _interactionDistance = 2.0f;
        [SerializeField] private LayerMask _interactionLayer;
        [SerializeField] private Transform _interactionPoint;
        [SerializeField] private Inventory.Inventory _inventory;

        private readonly Collider[] _interactionResults = new Collider[10];
        private Transform _heldObject;

        public void TryInteractOrDrop()
        {
            if (_heldObject != null)
            {
                DropHeldObject();
                return;
            }

            int hits = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionDistance, _interactionResults, _interactionLayer);

            if (hits > 0)
            {
                for (int i = 0; i < hits; i++)
                {
                    Collider hit = _interactionResults[i];
                    if (hit.TryGetComponent(out IInventoryItem inventoryItem))
                    {
                        if (_inventory.HasEmptySlot() && _inventory.AddItem(inventoryItem))
                        {
                            Debug.Log($"Added {hit.name} to inventory");
                            break;
                        }
                        Debug.Log("Inventory is full!");
                    }
                    else if (hit.TryGetComponent(out ITool tool))
                    {
                        if (_heldObject == null)
                        {
                            tool.PickUp(_interactionPoint);
                            _heldObject = hit.transform;
                            Debug.Log($"Picked up tool: {hit.name}");
                        }
                    }
                    else if (hit.TryGetComponent(out GardenBed gardenBed))
                    {
                        if (_inventory.HasItem<Seed>() && gardenBed.TryPlantSeed())
                        {
                            var seed = _inventory.GetItem<Seed>();
                            seed.MarkForPlanting();
                            _inventory.TryUseItem<Seed>();
                            Debug.Log("Seed planted successfully!");
                        }
                    }
                }
            }

            Array.Clear(_interactionResults, 0, _interactionResults.Length);
        }

        private void DropHeldObject()
        {
            Vector3 dropPosition = _interactionPoint.position + _interactionPoint.forward * 1.0f;
            _heldObject.GetComponent<ITool>().Drop(dropPosition);
            Debug.Log($"Dropped tool: {_heldObject.name}");
            _heldObject = null;
        }
    }
}