using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace _Project.Scripts.UI
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private RectTransform _slotContainer;
        [SerializeField] private GameObject _slotPrefab;
        [SerializeField] private Sprite _seedIcon;

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
                ClearSlot(slot.transform);
            }
        }

        public void UpdateSlots(List<ItemType> items)
        {
            for (int i = 0; i < _slotContainer.childCount; i++)
            {
                ClearSlot(_slotContainer.GetChild(i));
            }

            for (int i = 0; i < items.Count; i++)
            {
                if (i >= _slotContainer.childCount)
                {
                    Debug.LogError("Not enough slots to display all items!");
                    break;
                }

                Transform slot = _slotContainer.GetChild(i);
                Image icon = slot.GetComponentInChildren<Image>();
                Text countText = slot.GetComponentInChildren<Text>();

                if (icon != null)
                {
                    icon.sprite = GetIconForItem(items[i]);
                    icon.enabled = true;
                }

            }
        }

        private void ClearSlot(Transform slot)
        {
            Image icon = slot.GetComponentInChildren<Image>();
            Text countText = slot.GetComponentInChildren<Text>();

            if (icon != null)
            {
                icon.sprite = null;
                icon.enabled = false;
            }

            if (countText != null)
            {
                countText.text = "";
                countText.enabled = false;
            }
        }

        private Sprite GetIconForItem(ItemType itemType)
        {
            switch (itemType)
            {
                case ItemType.Seed:
                    return _seedIcon;
                default:
                    Debug.LogError($"No icon defined for {itemType}");
                    return null;
            }
        }
    }
}