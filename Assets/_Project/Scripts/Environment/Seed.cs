using UnityEngine;
using _Project.Scripts.Interface;

namespace _Project.Scripts.Environment
{
    public class Seed : MonoBehaviour, IInventoryItem
    {
        private const float XZ_RANGE = 0.5f;
        private const float Y_OFFSET = 0.5f;

        [Header("Settings")]
        [SerializeField] private GameObject _seedPrefab;
        private bool _isBeingUsedForPlanting;

        [field: SerializeField] public Sprite Icon { get; private set; }

        public GameObject GameObject => gameObject;

        public static void DropSeeds(Vector3 position, int count, GameObject seedPrefab)
        {
            for (int i = 0; i < count; i++)
            {
                Vector3 offset = new Vector3(
                    Random.Range(-XZ_RANGE, XZ_RANGE),
                    Y_OFFSET,
                    Random.Range(-XZ_RANGE, XZ_RANGE)
                );
                Instantiate(seedPrefab, position + offset, Quaternion.identity);
            }
        }

        public void OnPickUp() => gameObject.SetActive(false);

        public void OnDrop(Vector3 dropPosition)
        {
            if (_isBeingUsedForPlanting) return;
            
            transform.position = dropPosition;
            gameObject.SetActive(true);
        }

        public void MarkForPlanting()
        {
            _isBeingUsedForPlanting = true;
            Destroy(gameObject);
        }
    }
}