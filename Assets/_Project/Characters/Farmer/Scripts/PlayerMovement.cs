using UnityEngine;

namespace _Project.Characters.Farmer.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private PlayerStats playerStats;
        private Vector3 movement;

        private void Start()
        {
            mainCamera = Camera.main;
            playerStats = GetComponent<PlayerStats>();
        }

        public void Movement(Vector3 direction)
        {
            movement = direction.normalized;

            transform.Translate(movement * (playerStats.MovementSpeed * Time.deltaTime), Space.World);

            if (movement == Vector3.zero)
                return;

            transform.forward = movement;
        }
    }
}