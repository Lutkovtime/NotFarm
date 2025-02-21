using UnityEngine;

namespace _Project.Scripts
{
    public interface IInventoryItem
    {
        void OnPickUp();
        void OnDrop(Vector3 dropPosition);
        Sprite GetIcon();
    }
}