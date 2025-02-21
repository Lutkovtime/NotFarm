using _Project.Scripts;
using UnityEngine;

namespace _Project.Environment.Plants.Scripts
{
    public class Seed : MonoBehaviour, IInventoryItem
    {
        [SerializeField] private Sprite icon;
        [SerializeField] private string seedName = "Green Seeds";

        public Seed(string seedName)
        {
            SeedName = seedName;
        }


        public void OnPickUp()
        {
            gameObject.SetActive(false);
            Debug.Log($"{SeedName} added to inventory");
        }

        private string SeedName { get; }

        public void OnDrop(Vector3 dropPosition)
        {
            transform.position = dropPosition;
            gameObject.SetActive(true);
            Debug.Log($"{SeedName} removed from inventory");
        }

        public Sprite GetIcon()
        {
            return icon;
        }
    }
}