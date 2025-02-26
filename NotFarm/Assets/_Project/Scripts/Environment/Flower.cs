using UnityEngine;

namespace _Project.Scripts.Environment
{
    public class Flower : MonoBehaviour
    {
        [SerializeField] private Material[] growthStages; // Array to hold materials for each growth stage
        private int currentStage = 0; // Current growth stage
        private bool isPlanted = false; // Indicates if the flower is planted

        private void Start()
        {
            if (growthStages.Length > 0)
            {
                UpdateFlowerAppearance();
            }
        }

        private void Update()
        {
            if (isPlanted && currentStage < growthStages.Length - 1)
            {
                // Check if the garden bed is wet to grow the flower
                GardenBed gardenBed = GetComponentInParent<GardenBed>();
                if (gardenBed != null && gardenBed.IsWet)
                {
                    Grow();
                }
            }
        }

        public void PlantFlower()
        {
            isPlanted = true;
            UpdateFlowerAppearance();
        }

        private void Grow()
        {
            currentStage++;
            if (currentStage < growthStages.Length)
            {
                UpdateFlowerAppearance();
            }
        }

        private void UpdateFlowerAppearance()
        {
            if (currentStage < growthStages.Length)
            {
                GetComponent<MeshRenderer>().material = growthStages[currentStage];
            }
        }
    }
}