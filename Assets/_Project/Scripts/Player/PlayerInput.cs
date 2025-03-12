using UnityEngine;

namespace _Project.Scripts.Player
{
    public class PlayerInput : MonoBehaviour
    {
        private const string HORIZONTAL_AXIS = "Horizontal";
        private const string VERTICAL_AXIS = "Vertical";
        private const KeyCode INTERACTION_KEY = KeyCode.E;
        private const KeyCode INVENTORY_KEY = KeyCode.I;

        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerInteractions _playerInteractions;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private GameObject _inventoryUI;

        private void Update()
        {
            HandleMovementInput();
            HandleInteractionInput();
            HandleInventoryInput();
        }

        private void HandleMovementInput()
        {
            float horizontal = Input.GetAxisRaw(HORIZONTAL_AXIS);
            float vertical = Input.GetAxisRaw(VERTICAL_AXIS);

            Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
            _playerMovement.Move(direction);
        }

        private void HandleInteractionInput()
        {
            if (Input.GetKeyDown(INTERACTION_KEY))
            {
                _playerInteractions.TryInteractOrDrop();
            }
        }

        private void HandleInventoryInput()
        {
            if (Input.GetKeyDown(INVENTORY_KEY))
            {
                if (_inventoryUI != null)
                {
                    _inventoryUI.SetActive(!_inventoryUI.activeSelf);
                }
            }
        }
    }
}