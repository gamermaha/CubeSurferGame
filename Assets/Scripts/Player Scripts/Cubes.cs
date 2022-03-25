using UnityEngine;

namespace Player_Scripts
{
    
    public class Cubes : MonoBehaviour
    {
        public static bool OnPath;
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Path"))
                OnPath = true;
        }
        private void OnCollisionExit(Collision other)
        {
            if (other.gameObject.CompareTag("Path"))
            {
                OnPath = false;
            }
        }
    }
}
