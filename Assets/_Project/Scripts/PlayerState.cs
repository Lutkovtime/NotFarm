using UnityEngine;
using _Project.Characters.Farmer.Scripts;

namespace _Project.Scripts
{
    public class PlayerState : MonoBehaviour
    {
        [field: SerializeField] public Rigidbody Rb { get; private set; }
        [field: SerializeField] public GameObject CharacterModel { get; private set; }
        [field: SerializeField] public Characters.Farmer.Scripts.PlayerMovement PlayerMovement { get; private set; }
        [field: SerializeField] public PlayerInput PlayerInput { get; private set; }

        private void Awake()
        {
            if (Rb == null)
            {
                Rb = GetComponent<Rigidbody>();
            }

            if (CharacterModel == null)
            {
                CharacterModel = GetComponentInChildren<Transform>().gameObject;
            }
        }
    }
}
