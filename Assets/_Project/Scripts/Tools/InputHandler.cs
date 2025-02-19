using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] private Movement _movement;
    [SerializeField] private Transform _handTransform;

    [Header("Settings")]
    [SerializeField] private KeyCode _interactionKey = KeyCode.E;
    [SerializeField] private float _interactionRadius = 1.5f;

    private Bucket _currentBucket;
    private Collider[] _overlapResults = new Collider[3];

    private void Update()
    {
        HandleMovement();
        HandleBucketInteraction();
    }

    private void HandleMovement()
    {
        Vector3 direction = new Vector3(
            _joystick.Horizontal,
            0,
            _joystick.Vertical
        ).normalized;

        _movement.Move(direction);
    }

    private void HandleBucketInteraction()
    {
        if (Input.GetKeyDown(_interactionKey))
        {
            if (_currentBucket != null)
            {
                ReleaseBucket();
            }
            else
            {
                TryFindNearbyBucket();
            }
        }
    }

    private void TryFindNearbyBucket()
    {
        int count = Physics.OverlapSphereNonAlloc(
            transform.position,
            _interactionRadius,
            _overlapResults,
            LayerMask.GetMask("Bucket")
        );

        for (int i = 0; i < count; i++)
        {
            Bucket bucket = _overlapResults[i].GetComponent<Bucket>();
            if (bucket != null && !bucket.IsHeld)
            {
                GrabBucket(bucket);
                return;
            }
        }
    }

    private void GrabBucket(Bucket bucket)
    {
        _currentBucket = bucket;
        _currentBucket.PickUp(_handTransform);
    }

    private void ReleaseBucket()
    {
        _currentBucket.Drop();
        _currentBucket = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _interactionRadius);
    }
}