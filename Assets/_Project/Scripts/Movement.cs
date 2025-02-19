using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
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

    public void Move(Vector3 direction)
    {
        _moveDirection = direction.normalized;
    }

    private void FixedUpdate()
    {
        if (_moveDirection.sqrMagnitude > 0.01f)
        {
            Vector3 targetVelocity = _moveDirection * _moveSpeed;
            targetVelocity.y = _rigidbody.linearVelocity.y;

            _rigidbody.linearVelocity = targetVelocity;
            transform.forward = _moveDirection;
        }
        else
        {
            Vector3 currentVelocity = _rigidbody.linearVelocity;
            currentVelocity.x = 0f;
            currentVelocity.z = 0f;

            _rigidbody.linearVelocity = currentVelocity;
        }
    }

}

