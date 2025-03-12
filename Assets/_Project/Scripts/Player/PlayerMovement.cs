using UnityEngine;

namespace _Project.Scripts.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _gravity = -9.81f;

        private CharacterController _controller;
        private Vector3 _velocity;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            if (_controller == null)
            {
                Debug.LogError("CharacterController not found on PlayerMovement!");
            }
        }

        public void Move(Vector3 direction)
        {
            if (_controller == null)
            {
                return;
            }

            if (_controller.isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f;
            }

            _controller.Move(direction * (_moveSpeed * Time.deltaTime));

            _velocity.y += _gravity * Time.deltaTime;
            _controller.Move(_velocity * Time.deltaTime);
        }
    }
}