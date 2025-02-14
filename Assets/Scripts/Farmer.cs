using UnityEngine;

namespace FarmGame.Farmer
{
    public class Farmer : MonoBehaviour
    {
        [SerializeField] private Movement _movement;
        [SerializeField] private InputHandler _inputHandler;

        public Movement Movement => _movement;
        public InputHandler InputHandler => _inputHandler;
    }
}
