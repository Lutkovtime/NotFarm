using UnityEngine;
using _Project.Environment.Tools.Scripts;

namespace _Project.Environment.Farmland.Scripts
{
    public class GardenBed : MonoBehaviour
    {
        [SerializeField] private Material dryMaterial;
        [SerializeField] private Material wetMaterial;

        [SerializeField] private float dryTime = 30f;
        [SerializeField] private float interactionRadius = 2.0f;

        private bool isWet;
        private float wetTimer;
        private MeshRenderer _meshRenderer;

        private void Start()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            SetDry();
        }

        private void Update()
        {
            if (isWet)
            {
                wetTimer -= Time.deltaTime;
                if (wetTimer <= 0)
                {
                    SetDry();
                }
            }

            CheckAndWater();
        }

        private void SetDry()
        {
            isWet = false;
            _meshRenderer.material = dryMaterial;
            Debug.Log("The garden bed is now dry.");
        }

        private void SetWet()
        {
            isWet = true;
            wetTimer = dryTime;
            _meshRenderer.material = wetMaterial;
            Debug.Log("The garden bed is now wet.");
        }

        public void WaterPlot()
        {
            if (!isWet)
            {
                SetWet();
            }
        }

        private void CheckAndWater()
        {
            var results = new Collider[10];
            int size = Physics.OverlapSphereNonAlloc(transform.position, interactionRadius, results);

            for (int i = 0; i < size; i++)
            {
                if (results[i].TryGetComponent(out Bucket bucket))
                {
                    if (bucket.IsCarried && bucket.HasWater)
                    {
                        WaterPlot();
                        bucket.HasWater = false;
                        Debug.Log($"{bucket.name} watered the garden bed.");
                        return;
                    }
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, interactionRadius);
        }
    }
}