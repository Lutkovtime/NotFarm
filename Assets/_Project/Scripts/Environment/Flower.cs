using UnityEngine;

namespace _Project.Scripts.Environment
{
    public class Flower : MonoBehaviour
    {
        [Header("Growth Settings")]
        [SerializeField] private Transform _flowerTransform;
        [SerializeField] private float _timePerStage = 10f;
        [SerializeField] private Vector3[] _stageScales = { 
            new(0.1f, 0.1f, 0.1f), 
            new(0.5f, 0.5f, 0.5f), 
            new(1f, 1f, 1f)
        };

        [Header("Seed Settings")]
        [SerializeField] private Seed _seedPrefab; // Теперь Seed, а не GameObject
        [SerializeField] private int _seedsDropped = 2;
        [SerializeField] private Inventory _inventory; // Для добавления семян в инвентарь

        private int _currentStage;
        private float _growthTimer;
        private GardenBed _gardenBed;
        private bool _isFullyGrown;

        public bool IsFullyGrown => _isFullyGrown;

        public void Initialize(GardenBed gardenBed)
        {
            _gardenBed = gardenBed;
            Plant();
        }

        private void Start()
        {
            if (_flowerTransform == null)
            {
                Debug.LogError("Flower Transform is not assigned!");
                enabled = false;
                return;
            }
            _flowerTransform.localScale = _stageScales[0];
        }

        private void Update()
        {
            if (_isFullyGrown)
            {
                return;
            }

            _growthTimer += Time.deltaTime;
            if (_growthTimer >= _timePerStage)
            {
                _growthTimer = 0;
                ShowNextStage();
            }
        }

        private void ShowNextStage()
        {
            _currentStage++;
            if (_currentStage >= _stageScales.Length)
            {
                _isFullyGrown = true;
                Debug.Log("Flower is fully grown!");
                return;
            }
            _flowerTransform.localScale = _stageScales[_currentStage];
        }

        private void Plant()
        {
            Debug.Log("Flower planted!");
        }

        public void Harvest()
        {
            if (!_isFullyGrown)
            {
                Debug.Log("Not ready to harvest!");
                return;
            }
            DropSeeds();
            _gardenBed.RemoveFlower();
            Destroy(gameObject);
        }

        private void DropSeeds()
        {
            for (int i = 0; i < _seedsDropped; i++)
            {
                if (_inventory != null && _inventory.AddItem(ItemType.Seed))
                {
                    Debug.Log("Seed added to inventory!");
                }
                else
                {
                    Vector3 spawnPosition = transform.position + new Vector3(
                        Random.Range(-0.5f, 0.5f),
                        0.5f,
                        Random.Range(-0.5f, 0.5f)
                    );
                    Seed seed = Instantiate(_seedPrefab, spawnPosition, Quaternion.identity);
                    Debug.Log("Seed dropped on ground!");
                }
            }
        }
    }
}