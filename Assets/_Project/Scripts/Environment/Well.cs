using UnityEngine;

namespace _Project.Scripts.Environment
{
    public class Well : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _interactionRadius = 2.0f;

        private SphereCollider _triggerCollider;

        private void Start()
        {
            _triggerCollider = GetComponent<SphereCollider>();
            if (_triggerCollider == null)
            {
                _triggerCollider = gameObject.AddComponent<SphereCollider>();
            }
            _triggerCollider.isTrigger = true;
            _triggerCollider.radius = _interactionRadius;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out Bucket bucket) && !bucket.HasWater)
            {
                bucket.HasWater = true;
                Debug.Log($"Bucket {bucket.name} was filled in well trigger zone!");
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, _interactionRadius);
        }
    }
}