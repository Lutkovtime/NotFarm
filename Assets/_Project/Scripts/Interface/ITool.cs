using UnityEngine;

namespace _Project.Scripts.Interface
{
    public interface ITool
    {
        void PickUp(Transform handHoldPoint);
        void Drop(Vector3 dropPosition);
    }
}