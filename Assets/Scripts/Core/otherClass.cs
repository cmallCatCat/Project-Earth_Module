using UnityEngine;

namespace Core
{
    public class otherClass : MonoBehaviour
    {
        private void Awake()
        {
            InputReader.Instance.moveStart += Move;
        }

        private void Move(Vector2 value) { }
    }
}