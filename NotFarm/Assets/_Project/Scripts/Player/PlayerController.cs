using UnityEngine;

namespace _Project.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float interactionRadius = 2.0f;
        [SerializeField] private LayerMask gardenBedLayer;
        [SerializeField] private Seed seedPrefab;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                TryPlantSeed();
            }
        }

        private void TryPlantSeed()
        {
            Collider[] results = new Collider[10];
            int size = Physics.OverlapSphereNonAlloc(transform.position, interactionRadius, results, gardenBedLayer);

            for (int i = 0; i < size; i++)
            {
                if (results[i].TryGetComponent(out GardenBed gardenBed))
                {
                    if (HasSeedInInventory())
                    {
                        PlantSeed(gardenBed);
                        return;
                    }
                }
            }
        }

        private bool HasSeedInInventory()
        {
            // Implement your inventory check logic here
            return true; // Placeholder for actual inventory check
        }

        private void PlantSeed(GardenBed gardenBed)
        {
            Seed seed = Instantiate(seedPrefab, transform.position, Quaternion.identity);
            gardenBed.WaterPlot(); // Assuming the garden bed is watered when a seed is planted
            seed.OnPickUp(); // Simulate picking up the seed
            Debug.Log("Seed planted in the garden bed.");
        }
    }
}