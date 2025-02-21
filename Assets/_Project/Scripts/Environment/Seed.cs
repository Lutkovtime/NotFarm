using _Project.Scripts.Interface;
using UnityEngine;

namespace _Project.Scripts.Environment
{
    public class Seed : MonoBehaviour, IInventoryItem
    {
        [field: SerializeField] public Sprite Icon { get; private set; }

        public void OnPickUp()
        {
            gameObject.SetActive(false);
        }

        public void OnDrop(Vector3 dropPosition)
        {
            transform.position = dropPosition;
            gameObject.SetActive(true);
        }

        public Sprite GetIcon()
        {
            return Icon;
        }
    }
}