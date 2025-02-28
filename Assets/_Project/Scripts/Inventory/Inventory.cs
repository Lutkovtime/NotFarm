using System.Linq;
using _Project.Scripts.Interface;
using _Project.Scripts.UI;
using UnityEngine;

namespace _Project.Scripts.Inventory
{
    public class Inventory : MonoBehaviour
    {
        [field: SerializeField] public int MaxSlots { get; private set; } = 9;
        [SerializeField] private InventoryUI _inventoryUI;

        private IInventoryItem[] _items;

        private void Awake()
        {
            _items = new IInventoryItem[MaxSlots];

            if (_inventoryUI != null)
                _inventoryUI.InitializeSlots(MaxSlots);
            else
                Debug.LogError("InventoryUI reference is missing!");
        }

        public bool AddItem(IInventoryItem item)
        {
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i] == null)
                {
                    _items[i] = item;
                    item.OnPickUp();
                    _inventoryUI.UpdateSlot(i, item.GameObject);
                    return true;
                }
            }
            return false;
        }

        private IInventoryItem RemoveItem(int slot)
        {
            if (slot < 0 || slot >= MaxSlots || _items[slot] == null) return null;

            IInventoryItem item = _items[slot];
            _items[slot] = null;
            _inventoryUI.UpdateSlot(slot, null);
            return item;
        }

        public bool HasEmptySlot()
        {
            foreach (IInventoryItem slot in _items)
            {
                if (slot == null)
                    return true;
            }
            return false;
        }

        public IInventoryItem GetItemInSlot(int slot) => (slot < 0 || slot >= MaxSlots) ? null : _items[slot];

        public bool HasItem<T>() where T : IInventoryItem => _items.Any(item => item is T);

        public T GetItem<T>() where T : class => _items.FirstOrDefault(item => item is T) as T;

        public bool TryUseItem<T>() where T : IInventoryItem
        {
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i] is T)
                {
                    RemoveItem(i);
                    return true;
                }
            }
            return false;
        }
    }
}