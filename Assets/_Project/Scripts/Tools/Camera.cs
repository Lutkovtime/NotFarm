using UnityEngine;

namespace GameNamespace
{
    public class Camera : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _smoothSpeed = 5f;

        private Vector3 _offset;

        private void Start()
        {
            if (_target == null)
            {
                Debug.LogError("FarmerCamera: Target не назначен!");
                enabled = false;
                return;
            }

            _offset = transform.position - _target.position;
        }

        private void LateUpdate()
        {
            if (_target == null) return;

            Vector3 targetPosition = _target.position + _offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, _smoothSpeed * Time.deltaTime);
        }
    }
}
