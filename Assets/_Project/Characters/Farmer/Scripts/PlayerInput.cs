using _Project.Scripts;
using UnityEngine;

namespace _Project.Characters.Farmer.Scripts
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private PlayerState _playerState;
        [SerializeField] private PlayerMovement _playerMovement;

        private void Update()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector3(horizontal, 0, vertical);
            _playerMovement.Movement(direction);
        }
    }
}