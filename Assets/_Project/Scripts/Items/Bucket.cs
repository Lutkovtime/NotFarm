using UnityEngine;

public class Bucket : MonoBehaviour
{
    [SerializeField] private Material _filledMaterial;
    [SerializeField] private Material _emptyMaterial;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _throwForce = 2f;
    [SerializeField] private Renderer _bucketRenderer;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Collider _bucketCollider;
    private Transform _originalParent;
    private int _originalLayer;
    private bool _isHeld = false;
    private bool _isFilled = false;

    private void Awake()
    {
        _originalParent = transform.parent;
        _originalLayer = gameObject.layer;

    }

    public void PickUp(Transform holdPoint)
    {
        if (_isHeld) return;

        _isHeld = true;

        _rb.isKinematic = true;
        _bucketCollider.enabled = false;

        transform.SetParent(holdPoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        gameObject.layer = LayerMask.NameToLayer("NoCollision");
    }

    public void Drop()
    {
        if (!_isHeld) return;

        _isHeld = false;

        transform.SetParent(_originalParent);
        _rb.isKinematic = false;
        _bucketCollider.enabled = true;
        gameObject.layer = _originalLayer;

        if (_rb != null)
        {
            _rb.AddForce(transform.forward * _throwForce, ForceMode.Impulse);
        }

        SnapToGround();
    }

    private void SnapToGround()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 2f, _groundLayer))
        {
            transform.position = hit.point + Vector3.up * 0.1f;
        }
    }

    public void Fill()
    {
        _isFilled = true;
        UpdateVisuals();
        Debug.Log("Bucket filled");
    }

    public void Empty()
    {
        _isFilled = false;
        UpdateVisuals();
        Debug.Log("Bucket emptied");
    }

    private void UpdateVisuals()
    {
        if (_bucketRenderer != null)
        {
            _bucketRenderer.material = _isFilled ? _filledMaterial : _emptyMaterial;
        }
    }

    public bool IsHeld => _isHeld;
    public bool IsFilled => _isFilled;
}