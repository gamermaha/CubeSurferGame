using System;
using UnityEngine;

namespace Player_Scripts
{
    
    public class Cubes : MonoBehaviour
    {
        public static bool OnPath;

        private Vector3 newPos;
        private int _increment;
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Path"))
                OnPath = true;
           
            if (collision.gameObject.CompareTag("Cube"))
            {
                var transformPosition = transform.position;
                
                newPos = transformPosition;
                newPos.y += (0.25f - 2f);
                newPos.x = 0;
                newPos.z = 0;
                
                collision.transform.SetParent(transform.parent);
                collision.transform.localPosition = newPos;
                collision.gameObject.tag = "CubeAdded";
                
            }

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
