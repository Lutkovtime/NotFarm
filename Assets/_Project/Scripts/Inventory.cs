using System.Collections.Generic;
using UnityEngine;
using _Project.Scripts.UI;

namespace _Project.Scripts
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private int _maxSlots = 9;
        [SerializeField] private InventoryUI _inventoryUI;

        private List<ItemType> _items = new List<ItemType>();

        private void Awake()
        {
            if (_inventoryUI != null)
            {
                _inventoryUI.InitializeSlots(_maxSlots);
            }
            else
            {
                Debug.LogError("InventoryUI reference is missing!");
            }
        }

        public bool AddItem(ItemType itemType)
        {
            if (_items.Count >= _maxSlots)
            {
                Debug.Log("Inventory is full!");
                return false;
            }

            _items.Add(itemType);
            Debug.Log($"Added {itemType}. Total slots used: {_items.Count}");
            if (_inventoryUI != null)
            {
                _inventoryUI.UpdateSlots(_items);
            }
            return true;
        }

        public bool TryUseItem(ItemType itemType, out bool success)
        {
            if (_items.Contains(itemType))
            {
                _items.Remove(itemType);
                Debug.Log($"Used {itemType}. Remaining slots: {_items.Count}");
                if (_inventoryUI != null)
                {
                    _inventoryUI.UpdateSlots(_items);
                }
                success = true;
                return true;
            }

            Debug.Log($"No {itemType} in inventory!");
            success = false;
            return false;
        }
    }
}