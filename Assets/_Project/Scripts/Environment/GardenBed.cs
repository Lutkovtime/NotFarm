using UnityEngine;

namespace _Project.Scripts.Environment
{
    public class GardenBed : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _dryTime = 30f;
        [SerializeField] private float _interactionRadius = 2.0f;
        [SerializeField] private Material _dryMaterial;
        [SerializeField] private Material _wetMaterial;
        [SerializeField] private Flower _flowerPrefab;

        private float _wetTimer;
        private MeshRenderer _meshRenderer;
        private Flower _currentFlower;
        public bool IsWet { get; private set; }

        private void Start()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            SetDry();
        }

        private void Update()
        {
            if (IsWet)
            {
                _wetTimer -= Time.deltaTime;
                if (_wetTimer <= 0)
                {
                    SetDry();
                }
            }
        }

        private void SetDry()
        {
            IsWet = false;
            _meshRenderer.material = _dryMaterial;
            Debug.Log("The garden bed is now dry.");
        }

        private void SetWet()
        {
            IsWet = true;
            _wetTimer = _dryTime;
            _meshRenderer.material = _wetMaterial;
            Debug.Log("The garden bed is now wet.");
        }

        public void WaterPlot()
        {
            if (!IsWet)
            {
                SetWet();
            }
        }

        public bool TryPlantSeed()
        {
            if (_currentFlower != null)
            {
                Debug.Log("There is already a flower planted here.");
                return false;
            }

            if (!IsWet)
            {
                Debug.Log("The garden bed is dry. Water it first!");
                return false;
            }

            Vector3 flowerPosition = transform.position + new Vector3(0, 0.3f, 0);
            _currentFlower = Instantiate(_flowerPrefab, flowerPosition, Quaternion.identity);
            _currentFlower.Initialize(this);
            Debug.Log("Seed planted successfully!");
            return true;
        }

        public void RemoveFlower()
        {
            _currentFlower = null;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _interactionRadius);
        }
    }
}