using UnityEngine;
using _Project.Scripts.Interface;

namespace _Project.Scripts.Environment
{
    public class Bucket : MonoBehaviour, ITool
    {
        [Header("Settings")]
        [SerializeField] private Material _waterMaterial;
        [SerializeField] private Material _emptyMaterial;

        private MeshRenderer _bucketRenderer;

        public bool IsCarried { get; set; }
        public bool HasWater { get; set; }

        private void Start()
        {
            _bucketRenderer = GetComponent<MeshRenderer>();
            UpdateMaterial();
        }

        private void Update()
        {
            UpdateMaterial();
        }

        private void OnTriggerStay(Collider other)
        {
            if (!IsCarried || !HasWater) return;

            if (other.TryGetComponent(out GardenBed gardenBed) && !gardenBed.IsWet)
            {
                gardenBed.WaterPlot();
                HasWater = false;
                Debug.Log($"{name} automatically watered the garden bed.");
            }
        }

        public void PickUp(Transform handHoldPoint)
        {
            if (IsCarried) return;

            IsCarried = true;
            transform.SetParent(handHoldPoint);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }

        public void Drop(Vector3 dropPosition)
        {
            if (!IsCarried) return;

            IsCarried = false;
            transform.SetParent(null);

            if (Physics.Raycast(dropPosition, Vector3.down, out RaycastHit hit, 10f))
            {
                transform.position = hit.point + Vector3.up * 0.1f;
            }
            else
            {
                transform.position = dropPosition;
            }
        }

        private void UpdateMaterial()
        {
            _bucketRenderer.material = HasWater ? _waterMaterial : _emptyMaterial;
        }
    }
}