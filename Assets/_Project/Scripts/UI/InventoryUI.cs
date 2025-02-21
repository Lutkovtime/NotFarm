using _Project.Scripts.Interface;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private RectTransform slotContainer;
        [SerializeField] private GameObject slotPrefab;

        public void UpdateSlot(int slotIndex, GameObject item)
        {
            if (slotIndex < 0 || slotIndex >= slotContainer.childCount)
            {
                return;
            }

            Transform slot = slotContainer.GetChild(slotIndex);
            if (slot is null)
            {
                return;
            }

            Image icon = slot.GetComponent<Image>();
            if (icon is null)
            {
                return;
            }

            if (item is not null && item.TryGetComponent(out IInventoryItem inventoryItem))
            {
                icon.sprite = inventoryItem.GetIcon();
                icon.enabled = true;
            }
            else
            {
                icon.sprite = null;
                icon.enabled = false;
            }
        }
    }
}
