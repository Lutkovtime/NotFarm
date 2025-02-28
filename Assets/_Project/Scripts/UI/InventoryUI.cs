using _Project.Scripts.Interface;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private RectTransform _slotContainer;
        [SerializeField] private GameObject _slotPrefab;

        public void InitializeSlots(int slotCount)
        {
            foreach (Transform child in _slotContainer)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < slotCount; i++)
            {
                GameObject slot = Instantiate(_slotPrefab, _slotContainer);
                slot.name = $"Slot_{i}";
                UpdateSlot(i, null);
            }
        }

        public void UpdateSlot(int slotIndex, GameObject item)
        {
            if (slotIndex < 0 || slotIndex >= _slotContainer.childCount)
            {
                Debug.LogError($"Invalid slot index: {slotIndex}");
                return;
            }

            Transform slot = _slotContainer.GetChild(slotIndex);
            Image icon = slot.GetComponent<Image>();

            if (item != null && item.TryGetComponent(out IInventoryItem inventoryItem))
            {
                icon.sprite = inventoryItem.Icon;
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