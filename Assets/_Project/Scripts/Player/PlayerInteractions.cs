using System;
using _Project.Scripts.Interface;
using _Project.Scripts.UI;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class PlayerInteractions : MonoBehaviour
    {
        [SerializeField] private float interactionDistance = 2.0f;
        [SerializeField] private LayerMask interactionLayer;
        [SerializeField] private Transform interactionPoint;
        [SerializeField] private Inventory.Inventory inventory;
        [SerializeField] private GameObject inventoryCanvas;
        [SerializeField] private InventoryUI inventoryUI;

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

            inventoryCanvas?.SetActive(_isInventoryOpen);

        }

        private void TryInteract()
        {
            int hits = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionDistance, _interactionResults, interactionLayer);

            if (hits > 0)
            {
                foreach (Collider hit in _interactionResults)
                {
                    if (hit is null)
                    {
                        continue;
                    }

                    if (hit.TryGetComponent(out IInventoryItem inventoryItem))
                    {
                        if (inventory.HasEmptySlot())
                        {
                            if (inventory.AddItem(inventoryItem))
                            {
                                hit.gameObject.SetActive(false);
                                Debug.Log($"Added {hit.name} to inventory");
                                break;
                            }
                            Debug.Log("Inventory is full!");
                        }
                    }
                    else if (hit.TryGetComponent(out ITool tool))
                    {
                        if (_heldObject is null)
                        {
                            tool.PickUp(interactionPoint);
                            _heldObject = hit.transform;
                            Debug.Log($"Picked up tool: {hit.name}");
                            break;
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
            Vector3 dropPosition = interactionPoint.position + interactionPoint.forward * 1.0f;
            tool.Drop(dropPosition);
            Debug.Log($"Dropped tool: {_heldObject.name}");
        }

        _heldObject = null;
        }
    }
}