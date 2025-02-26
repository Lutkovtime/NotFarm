using UnityEngine;

namespace _Project.Scripts.Environment
{
    public class Flower : MonoBehaviour
    {
        [field: SerializeField] public GameObject[] GrowthStages { get; private set; }
        [field: SerializeField] public float TimePerStage { get; private set; } = 10f;

        private int _currentStage = -1;
        private float _growthTimer;
        private GardenBed _gardenBed;

        public void Initialize(GardenBed gardenBed)
        {
            _gardenBed = gardenBed;
            Plant();
        }

        private void Start()
        {
            if (GrowthStages == null || GrowthStages.Length != 3)
            {
                Debug.LogError("Growth stages not properly configured!");
                enabled = false;
                return;
            }

            // Скрываем все стадии при старте
            foreach (var stage in GrowthStages)
            {
                stage.SetActive(false);
            }
        }

        private void Update()
        {
            if (_currentStage >= GrowthStages.Length - 1) return;

            if (_gardenBed.IsWet)
            {
                _growthTimer += Time.deltaTime;

                if (_growthTimer >= TimePerStage)
                {
                    _growthTimer = 0;
                    ShowNextStage();
                }
            }

            Debug.Log($"Time until next stage: {TimePerStage - _growthTimer:F1} seconds");
        }

        private void ShowNextStage()
        {
            if (_currentStage >= 0)
                GrowthStages[_currentStage].SetActive(false);

            _currentStage++;
            GrowthStages[_currentStage].SetActive(true);

            Debug.Log($"Flower advanced to stage {_currentStage + 1}");
        }

        public void Plant()
        {
            // Показываем первую стадию сразу после посадки
            ShowNextStage();
            Debug.Log("Flower planted!");
        }
    }
}