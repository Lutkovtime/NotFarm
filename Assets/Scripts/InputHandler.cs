using UnityEngine;
using FarmGame.Farmer.Tools;

namespace FarmGame.Farmer
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] private FloatingJoystick _joystick;
        [SerializeField] private Movement _movement;
        [SerializeField] private KeyCode _interactionKey = KeyCode.E;

        private Bucket _nearbyBucket;
        private Bucket _heldBucket;

        private void Update()
        {
            HandleMovementInput();
            HandleInteractionInput();
        }

        private void HandleMovementInput()
        {
            Vector2 moveInput = new Vector2(_joystick.Horizontal, _joystick.Vertical);
            _movement.Move(moveInput);
        }

        private void HandleInteractionInput()
        {
            if (Input.GetKeyDown(_interactionKey))
            {
                if (_heldBucket != null)
                {
                    _heldBucket.Drop();
                    _heldBucket = null;
                }
                else if (_nearbyBucket != null)
                {
                    _heldBucket = _nearbyBucket;
                    _heldBucket.PickUp(_movement.HandTransform);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            Bucket bucket = other.GetComponent<Bucket>();
            if (bucket != null && _heldBucket == null)
            {
                Debug.Log("Bucket found!");
                _nearbyBucket = bucket;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            Bucket bucket = other.GetComponent<Bucket>();
            if (bucket != null)
            {
                _nearbyBucket = null;
            }
        }
    }
}

