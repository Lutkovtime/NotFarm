using UnityEngine;

namespace FarmGame.Farmer.Tools
{
    public class Bucket : MonoBehaviour
    {
        private Rigidbody _rb;
        private bool _isHeld = false;
        private bool _isFilled = false;
        public Renderer bucketRenderer;
        public Material filledMaterial;
        public Material emptyMaterial;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        public void PickUp(Transform holdPoint)
        {
            _isHeld = true;
            _rb.isKinematic = true;
            transform.SetParent(holdPoint);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            GetComponent<Collider>().enabled = false;
        }

        public void Drop()
        {
            _isHeld = false;
            _rb.isKinematic = false;
            transform.SetParent(null);
            GetComponent<Collider>().enabled = true;

            _rb.AddForce(transform.forward * 2f, ForceMode.Impulse);
        }

        public void Fill()
        {
            _isFilled = true;
            Debug.Log("The bucket is filled with water!");
            bucketRenderer.material = filledMaterial;
        }

        public void Empty()
        {
            _isFilled = false;
            Debug.Log("The bucket is empty!");
            bucketRenderer.material = emptyMaterial;
        }

        public bool IsHeld => _isHeld;
        public bool IsFilled => _isFilled;
    }
}
