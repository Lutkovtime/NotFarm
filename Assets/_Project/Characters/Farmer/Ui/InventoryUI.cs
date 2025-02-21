using _Project.Characters.Farmer.Scripts;
using _Project.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Characters.Farmer.Ui
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private Inventory inventory;
        [SerializeField] private RectTransform slotContainer;
        [SerializeField] private GameObject slotPrefab;

        public void CreateSlot(int slotIndex, GameObject item)
        {
            GameObject newSlot = Instantiate(slotPrefab, slotContainer);

            Image icon = newSlot.GetComponent<Image>();
            if (item.TryGetComponent(out IInventoryItem inventoryItem))
            {
                icon.sprite = inventoryItem.GetIcon();
                icon.enabled = true;
            }
            else
            {
                icon.enabled = false;
            }
        }

        public void UpdateSlot(int slotIndex, GameObject item)
        {
            Transform slot = slotContainer.GetChild(slotIndex);
            Image icon = slot.GetComponent<Image>();

            if (item is not null && item.TryGetComponent(out IInventoryItem inventoryItem))
            {
                icon.sprite = inventoryItem.GetIcon();
                icon.enabled = true;
            }
            else
            {
                icon.enabled = false;
            }
        }
    }
}