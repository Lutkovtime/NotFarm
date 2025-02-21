using UnityEngine;

namespace _Project.Scripts.Environment
{
    public class Well : MonoBehaviour
    {
        [SerializeField] private float interactionRadius = 2.0f;

        private void Update()
        {
            TryFillBucketInHand();
        }

        private void TryFillBucketInHand()
        {
            var results = new Collider[10];
            int size = Physics.OverlapSphereNonAlloc(transform.position, interactionRadius, results);


            for (int i = 0; i < size; i++)
            {
                if (!results[i].TryGetComponent(out Bucket bucket))
                {
                    continue;
                }
                
                if (!bucket.IsCarried || bucket.HasWater)
                {
                    continue;
                }

                bucket.HasWater = true;
                Debug.Log($"Bucket {bucket.name} was filled while being carried!");
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, interactionRadius);
        }
    }
}