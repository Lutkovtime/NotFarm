using System;
using _Project.Scripts.Environment;
using _Project.Scripts.Interface;
using _Project.Scripts.UI;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class PlayerInteractions : MonoBehaviour
    {
        [field: SerializeField] public float InteractionDistance { get; private set; } = 2.0f;
        [field: SerializeField] public LayerMask InteractionLayer { get; private set; }
        [field: SerializeField] public Transform InteractionPoint { get; private set; }
        [field: SerializeField] public Inventory.Inventory Inventory { get; private set; }
        [field: SerializeField] public GameObject InventoryCanvas { get; private set; }
        [field: SerializeField] public InventoryUI InventoryUI { get; private set; }

        private readonly Collider[] _interactionResults = new Collider[10];
        private Transform _heldObject;
        private bool _isInventoryOpen;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                ToggleInventory();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (_heldObject is null)
                {
                    TryInteract();
                }
                else
                {
                    DropHeldObject();
                }
            }
        }

        private void ToggleInventory()
        {
            _isInventoryOpen = !_isInventoryOpen;
            InventoryCanvas?.SetActive(_isInventoryOpen);
        }

        private void TryInteract()
        {
            int hits = Physics.OverlapSphereNonAlloc(InteractionPoint.position, InteractionDistance, _interactionResults, InteractionLayer);

            if (hits > 0)
            {
                foreach (Collider hit in _interactionResults)
                {
                    if (hit is null) continue;

                    if (hit.TryGetComponent(out IInventoryItem inventoryItem))
                    {
                        if (Inventory.HasEmptySlot() && Inventory.AddItem(inventoryItem))
                        {
                            hit.gameObject.SetActive(false);
                            Debug.Log($"Added {hit.name} to inventory");
                            break;
                        }
                        Debug.Log("Inventory is full!");
                    }
                    else if (hit.TryGetComponent(out ITool tool))
                    {
                        if (_heldObject is null)
                        {
                            tool.PickUp(InteractionPoint);
                            _heldObject = hit.transform;
                            Debug.Log($"Picked up tool: {hit.name}");
                        }
                    }
                    else if (hit.TryGetComponent(out GardenBed gardenBed))
                    {
                        if (Inventory.HasItem<Seed>() && gardenBed.TryPlantSeed())
                        {
                            Inventory.TryUseItem<Seed>();
                            Debug.Log("Seed planted successfully!");
                        }
                        else
                        {
                            Debug.Log("No seeds in inventory or garden bed is not ready for planting.");
                        }
                    }
                }
            }

            Array.Clear(_interactionResults, 0, _interactionResults.Length);
        }

        private void DropHeldObject()
        {
            if (_heldObject is null) return;

            if (_heldObject.TryGetComponent(out ITool tool))
            {
                Vector3 dropPosition = InteractionPoint.position + InteractionPoint.forward * 1.0f;
                tool.Drop(dropPosition);
                Debug.Log($"Dropped tool: {_heldObject.name}");
            }

            _heldObject = null;
        }
    }
}