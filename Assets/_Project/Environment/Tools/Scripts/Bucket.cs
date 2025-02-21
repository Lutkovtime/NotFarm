using UnityEngine;
using _Project.Scripts;

namespace _Project.Environment.Tools.Scripts
{
    public class Bucket : MonoBehaviour, ITool
    {
        public bool HasWater
        {
            get
            {
                return _hasWater;
            }
            set
            {
                _hasWater = value;
                UpdateMaterial();
            }
        }

        private bool _hasWater;
        public bool IsCarried { get; private set; }

        [SerializeField] private Material waterMaterial;
        [SerializeField] private Material emptyMaterial;

        private MeshRenderer _bucketRenderer;
        private Rigidbody _rigidbody;

        private void Start()
        {
            _bucketRenderer = GetComponent<MeshRenderer>();
            _rigidbody = GetComponent<Rigidbody>();
            UpdateMaterial();
        }

        public void PickUp(Transform handHoldPoint)
        {
            if (IsCarried) return;

            IsCarried = true;
            transform.SetParent(handHoldPoint);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;

            _rigidbody.isKinematic = true;
        }

        public void Drop(Vector3 dropPosition)
        {
            if (!IsCarried) return;

            IsCarried = false;
            transform.SetParent(null);
            _rigidbody.isKinematic = false;
            transform.position = dropPosition;
        }

        private void UpdateMaterial()
        {
            if (_bucketRenderer is null) return;
            _bucketRenderer.material = HasWater ? waterMaterial : emptyMaterial;
        }
    }
}