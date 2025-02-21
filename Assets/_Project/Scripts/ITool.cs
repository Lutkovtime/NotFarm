using UnityEngine;

namespace _Project.Scripts
{
    public interface ITool
    {
        void PickUp(Transform handHoldPoint);
        void Drop(Vector3 dropPosition);
    }
}