using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Managers;
using TMPro.SpriteAssetUtilities;
using UnityEngine;

namespace Player_Scripts
{
    
    public class Cubes : MonoBehaviour
    {
        public static bool OnPath;

        private Vector3 _newPos;
        private double cubeSize;
        private int _increment;
        //private List<GameObject> _cubesAdded = new List<GameObject>(10);
        void Start()
        {
            cubeSize = MetaData.Instance.scriptableInstance.cubeLength;
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Path"))
                OnPath = true;
           
            if (collision.gameObject.CompareTag("Cube"))
            {
                //_cubesAdded.Add(collision.gameObject);

                var transformPosition = transform.position;
                _newPos = transformPosition;
                _newPos.y += (float) cubeSize - 2f;
                _newPos.x = 0;
                _newPos.z = 0;
                
                collision.transform.SetParent(transform.parent);
                collision.transform.localPosition = _newPos;
                
                collision.gameObject.tag = "CubeAdded";
                
                

            }
            
            if (collision.gameObject.CompareTag("CubeDestroy"))
            {
                if (transform.parent.childCount > 0)
                {
                    Destroy(transform.parent.GetChild(transform.parent.childCount - 1).gameObject);
                    //Destroy(_cubesAdded[_cubesAdded.Count - 1]);
                }
                else
                    Destroy(gameObject);
                
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
