using UnityEngine;

namespace _Project.Scripts.Interface
{
    public interface IInventoryItem
    {
        GameObject GameObject { get; }
        Sprite Icon { get; }
        void OnPickUp();
        void OnDrop(Vector3 dropPosition);
    }
}