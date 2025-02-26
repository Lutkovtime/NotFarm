using System.Linq;
using _Project.Scripts.Interface;
using _Project.Scripts.UI;
using UnityEngine;

namespace _Project.Scripts.Inventory
{
    public class Inventory : MonoBehaviour
    {
        [field: SerializeField] public int MaxSlots { get; private set; } = 9;
        [SerializeField] private InventoryUI inventoryUI;

        private IInventoryItem[] _items;

        private void Awake()
        {
            _items = new IInventoryItem[MaxSlots];
        }

        public bool AddItem(IInventoryItem item)
        {
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i] == null)
                {
                    _items[i] = item;
                    item.OnPickUp();

                    inventoryUI?.UpdateSlot(i, (item as MonoBehaviour)?.gameObject);
                    return true;
                }
            }
            return false;
        }

        public IInventoryItem RemoveItem(int slot)
        {
            if (slot < 0 || slot >= MaxSlots || _items[slot] == null) return null;

            IInventoryItem item = _items[slot];
            _items[slot] = null;
            item.OnDrop(transform.position);

            inventoryUI?.UpdateSlot(slot, null);
            return item;
        }

        public bool HasEmptySlot()
        {
            return _items.Any(slot => slot == null);
        }

        public IInventoryItem GetItemInSlot(int slot)
        {
            if (slot < 0 || slot >= MaxSlots) return null;
            return _items[slot];
        }

        public bool HasItem<T>() where T : IInventoryItem
        {
            return _items.Any(item => item is T);
        }

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