using UnityEngine;

namespace _Project.Scripts.Interface
{
    public interface ITool
    {
        bool IsCarried { get; set; }
        void PickUp(Transform handHoldPoint);
        void Drop(Vector3 dropPosition);
    }
}