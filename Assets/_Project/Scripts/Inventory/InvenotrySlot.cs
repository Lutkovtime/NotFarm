using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image _iconImage;
    [SerializeField] private Button _removeButton;

    private Item _item;
    private Inventory _inventory;

    public void Initialize(Inventory inventory, Item item)
    {
        _inventory = inventory;
        _item = item;
        _iconImage.sprite = _item.Icon;
        _iconImage.enabled = true;
        _removeButton.onClick.AddListener(RemoveItem);
    }

    private void RemoveItem()
    {
        _inventory.TryRemoveItem(_item);
        Destroy(gameObject);
    }
}
