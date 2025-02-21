using UnityEngine;

namespace _Project.Scripts.Player
{
    [System.Serializable]
    public class PlayerStats : MonoBehaviour
    {
    [field: SerializeField] public float MovementSpeed { get; private set; } = 10f;
    }
}