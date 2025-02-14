using UnityEngine;

namespace FarmGame.Farmer
{
    [RequireComponent(typeof(Rigidbody))]
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _rotationSpeed = 15f;
        [SerializeField] private Transform _handTransform;
        private Rigidbody _rigidbody;
        private Vector3 _moveDirection;

        public Transform HandTransform => _handTransform;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.freezeRotation = true;
            _rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
        }

        public void Move(Vector2 moveInput)
        {
            _moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
        }

        private void FixedUpdate()
        {
            HandleMovement();
            HandleRotation();
        }

        private void HandleMovement()
        {
            Vector3 targetVelocity = _moveDirection * _moveSpeed;
            targetVelocity.y = _rigidbody.linearVelocity.y;

            Vector3 velocityChange = targetVelocity - _rigidbody.linearVelocity;
            _rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
        }

        private void HandleRotation()
        {
            if (_moveDirection == Vector3.zero) return;

            float targetAngle = Mathf.Atan2(_moveDirection.x, _moveDirection.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                targetRotation,
                _rotationSpeed * Time.fixedDeltaTime
            );
        }
    }
}











