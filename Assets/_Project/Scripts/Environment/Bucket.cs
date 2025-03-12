using UnityEngine;

namespace _Project.Scripts.Environment
{
    [RequireComponent(typeof(MeshRenderer))]
    public class Bucket : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private Material _waterMaterial;
        [SerializeField] private Material _emptyMaterial;
        [SerializeField] private float _pickupRadius = 1.5f;

        private MeshRenderer _bucketRenderer;
        private Transform _handHoldPoint;

        public bool IsCarried { get; set; }
        public bool HasWater { get; set; }

        private void Awake()
        {
            _bucketRenderer = GetComponent<MeshRenderer>();
            if (_bucketRenderer == null)
            {
                Debug.LogError("MeshRenderer not found on Bucket!");
            }
        }

        private void Update()
        {
            if (!IsCarried)
            {
                var colliders = Physics.OverlapSphere(
                    transform.position,
                    _pickupRadius,
                    LayerMask.GetMask("Player")
                );

                if (colliders.Length > 0)
                {
                    PickUp(colliders[0].transform);
                }
            }

            if (IsCarried && _handHoldPoint != null)
            {
                transform.position = _handHoldPoint.position;
                transform.rotation = _handHoldPoint.rotation;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (!IsCarried)
            {
                return;
            }
            if (!HasWater)
            {
                return;
            }

            if (other.TryGetComponent(out GardenBed gardenBed) && !gardenBed.IsWet)
            {
                gardenBed.WaterPlot();
                Empty();
                Debug.Log($"{name} automatically watered the garden bed.");
            }
        }

        public void PickUp(Transform handHoldPoint)
        {
            if (IsCarried)
            {
                return;
            }
            if (handHoldPoint == null)
            {
                Debug.LogError("Hand hold point is null!");
                return;
            }

            IsCarried = true;
            _handHoldPoint = handHoldPoint;
            transform.SetParent(_handHoldPoint);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            Debug.Log("Bucket picked up!");
        }

        public void Drop(Vector3 dropPosition)
        {
            if (!IsCarried)
            {
                return;
            }

            IsCarried = false;
            _handHoldPoint = null;
            transform.SetParent(null);

            if (Physics.Raycast(dropPosition, Vector3.down, out RaycastHit hit, 10f))
            {
                transform.position = hit.point + Vector3.up * 0.1f;
            }
            else
            {
                transform.position = dropPosition;
            }
            Debug.Log("Bucket dropped!");
        }

        public void Fill()
        {
            if (HasWater)
            {
                return;
            }

            HasWater = true;
            _bucketRenderer.material = _waterMaterial;
            Debug.Log("Bucket filled with water!");
        }

        public void Empty()
        {
            if (!HasWater)
            {
                return;
            }

            HasWater = false;
            _bucketRenderer.material = _emptyMaterial;
            Debug.Log("Bucket emptied!");
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _pickupRadius);
        }
    }
}