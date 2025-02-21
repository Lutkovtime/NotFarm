using UnityEngine;

namespace _Project.Scripts
{
    [ExecuteInEditMode]
    public class TopDownCamera : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _smoothSpeed = 5f;
        [SerializeField] private Vector3 _offset = new Vector3(0, 10, -10);
        [SerializeField] private float _cameraAngle = 60f;

        private void Start()
        {
            UpdateCameraPosition();
        }

        private void LateUpdate()
        {
            if (_target is null) return;

            Vector3 targetPosition = _target.position + _offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, _smoothSpeed * Time.deltaTime);
        }

        private void OnValidate()
        {
            UpdateCameraPosition();
        }

        private void UpdateCameraPosition()
        {
            if (_target == null)
                return;
            transform.position = _target.position + _offset;
            transform.rotation = Quaternion.Euler(_cameraAngle, 0, 0);
        }
    }
}