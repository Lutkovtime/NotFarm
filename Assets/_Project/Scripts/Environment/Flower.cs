using UnityEngine;

namespace _Project.Scripts.Environment
{
    public class Flower : MonoBehaviour
    {
        [Header("Growth Settings")]
        [SerializeField] private Transform _flowerTransform;
        [SerializeField] private float _timePerStage = 10f;
        [SerializeField] private Vector3[] _stageScales = { 
            new Vector3(0.1f, 0.1f, 0.1f), 
            new Vector3(0.5f, 0.5f, 0.5f), 
            new Vector3(1f, 1f, 1f)
        };

        [Header("Seed Settings")]
        [SerializeField] private GameObject _seedPrefab;
        [SerializeField] private int _seedsDropped = 2;

        private int _currentStage;
        private float _growthTimer;
        private GardenBed _gardenBed;
        private bool _isFullyGrown;

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
            _currentStage = 0;
        }

        private void Update()
        {
            if (_isFullyGrown) return;

            if (_gardenBed.IsWet)
            {
                _growthTimer += Time.deltaTime;

                if (_growthTimer >= _timePerStage)
                {
                    _growthTimer = 0;
                    ShowNextStage();
                }
            }
        }

        private void ShowNextStage()
        {
            _currentStage++;

            if (_currentStage >= _stageScales.Length)
            {
                _isFullyGrown = true;
                Debug.Log("Flower is fully grown and ready to harvest!");
            }
            else
            {
                _flowerTransform.localScale = _stageScales[_currentStage];
                Debug.Log($"Flower advanced to stage {_currentStage + 1}");
            }
        }

        private void Plant() => Debug.Log("Flower planted!");

        public void Harvest()
        {
            if (!_isFullyGrown)
            {
                Debug.Log("Flower is not ready to harvest yet!");
                return;
            }

            DropSeeds();
            Destroy(gameObject);
            Debug.Log("Flower harvested!");
        }

        private void DropSeeds() => Seed.DropSeeds(transform.position, _seedsDropped, _seedPrefab);
    }
}