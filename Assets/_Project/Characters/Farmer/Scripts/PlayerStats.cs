using UnityEngine;

namespace _Project.Characters.Farmer.Scripts
{
    [System.Serializable]
    public class PlayerStats : MonoBehaviour
    {
    [field: SerializeField] public float MovementSpeed { get; private set; } = 10f;
    }
}