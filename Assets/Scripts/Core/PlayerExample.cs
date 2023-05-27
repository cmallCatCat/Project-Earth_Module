using UnityEngine;

namespace Core
{
    public class PlayerExample : MonoBehaviour
    {
        public float speed = 3;

        private void Start()
        {
        }

        private void Update()
        {
            float hor = Input.GetAxis("Horizontal");
            float ver = Input.GetAxis("Vertical");
            if (hor != 0 || ver != 0)
            {
                transform.Translate(hor * Time.deltaTime * speed, ver * Time.deltaTime * speed, 0);
            }
        }
    }

}