using UnityEngine;

namespace _Project.Scripts.Environment
{
    public class GardenBed : MonoBehaviour
    {
        [field: SerializeField] public bool IsWet { get; private set; }
        [field: SerializeField] public float DryTime { get; private set; } = 30f;
        [field: SerializeField] public float InteractionRadius { get; private set; } = 2.0f;

        [SerializeField] private Material dryMaterial;
        [SerializeField] private Material wetMaterial;
        [SerializeField] private Flower flowerPrefab;

        private float _wetTimer;
        private MeshRenderer _meshRenderer;
        private Flower _currentFlower;

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

            CheckAndWater();
        }

        private void SetDry()
        {
            IsWet = false;
            _meshRenderer.material = dryMaterial;
            Debug.Log("The garden bed is now dry.");
        }

        private void SetWet()
        {
            IsWet = true;
            _wetTimer = DryTime;
            _meshRenderer.material = wetMaterial;
            Debug.Log("The garden bed is now wet.");
        }

        public void WaterPlot()
        {
            if (!IsWet)
            {
                SetWet();
            }
        }

        private void CheckAndWater()
        {
            var results = new Collider[10];
            int size = Physics.OverlapSphereNonAlloc(transform.position, InteractionRadius, results);

            for (int i = 0; i < size; i++)
            {
                if (results[i].TryGetComponent(out Bucket bucket) && bucket.IsCarried && bucket.HasWater)
                {
                    WaterPlot();
                    bucket.HasWater = false;
                    Debug.Log($"{bucket.name} watered the garden bed.");
                    return;
                }
            }
        }

        public bool TryPlantSeed()
        {
            if (_currentFlower is not null)
            {
                Debug.Log("There is already a flower planted here.");
                return false;
            }

            if (!IsWet)
            {
                Debug.Log("The garden bed is dry. Water it first!");
                return false;
            }

            // Создаем цветок по центру грядки
            _currentFlower = Instantiate(flowerPrefab, transform.position, Quaternion.identity);
            _currentFlower.Initialize(this);
            Debug.Log("Seed planted successfully!");
            return true;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, InteractionRadius);
        }
    }
}