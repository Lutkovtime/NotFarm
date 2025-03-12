using UnityEngine;

namespace _Project.Scripts.Environment
{
    public class Seed : MonoBehaviour
    {
        [SerializeField] private float _pickupRadius = 1.5f;
        [SerializeField] private Inventory _inventory;

        private void Update()
        {
            var colliders = Physics.OverlapSphere(
                transform.position,
                _pickupRadius,
                LayerMask.GetMask("Player")
            );

            if (colliders.Length > 0)
            {
                OnPickUp();
            }
        }

        private void OnPickUp()
        {
            if (_inventory != null && _inventory.AddItem(ItemType.Seed))
            {
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Failed to pick up seed!");
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _pickupRadius);
        }
    }
}