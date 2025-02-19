using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int _maxSlots = 5;
    [SerializeField] private float _slotSpacing = 10f;

    [Header("References")]
    [SerializeField] private Transform _slotsContainer;
    [SerializeField] private GameObject _slotPrefab;

    private List<Item> _items = new List<Item>();
    private RectTransform _containerRect;

    private void Awake()
    {
        _containerRect = _slotsContainer.GetComponent<RectTransform>();
        UpdateLayout();
    }

    public bool TryAddItem(Item item)
    {
        if (_items.Count >= _maxSlots)
            return false;

        _items.Add(item);
        CreateNewSlot(item);
        UpdateLayout();
        return true;
    }

    public bool TryRemoveItem(Item item)
    {
        if (!_items.Contains(item))
            return false;

        _items.Remove(item);
        UpdateLayout();
        return true;
    }

    private void CreateNewSlot(Item item)
    {
        GameObject slotInstance = Instantiate(_slotPrefab, _slotsContainer);
        InventorySlot slot = slotInstance.GetComponent<InventorySlot>();
        slot.Initialize(this, item);
    }

    private void UpdateLayout()
    {
        float slotWidth = _slotPrefab.GetComponent<RectTransform>().sizeDelta.x;
        float totalWidth = (_items.Count * slotWidth) + ((_items.Count - 1) * _slotSpacing);
        _containerRect.sizeDelta = new Vector2(totalWidth, _containerRect.sizeDelta.y);
    }
}
