using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Managers;
using TMPro.SpriteAssetUtilities;
using UnityEngine;
using System.Linq;

namespace Player_Scripts
{
    public class PlayerCollider : MonoBehaviour
    {
        public static bool AddCube;
        public static bool DestroyCube;

        [SerializeField] private GameObject _cubeCollector;

        private Vector3 _newPos;
        private Vector3 _cubePos;
        private double _cubeSize;
        
        


        private void Start()
        {
            _cubeSize = MetaData.Instance.scriptableInstance.cubeLength;
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Cube count is : " + _cubeCollector.transform.childCount);
            if (other.gameObject.CompareTag("Cube"))
            {
                
                Debug.Log("I have encountered a cube to be added");
                
                AddCube = true;
                var transformPosition1 = transform.parent.position;
                _newPos = transformPosition1;
                _newPos.y += ((float) _cubeSize / 2 - 1f);
                _newPos.x = 0;
                _newPos.z = 0;
                    
                other.transform.SetParent(_cubeCollector.transform);
                other.transform.localPosition = _newPos;

                other.gameObject.tag = "CubeAdded";

                transform.localScale += new Vector3(0f, (float) _cubeSize, 0f);
                
            }
            if (other.gameObject.CompareTag("CubeDestroy"))
            {
                
                Debug.Log("I have encountered a cube to be destroyed");
                if (_cubeCollector.transform.childCount > 0)
                {
                    DestroyCube = true;
                    Destroy(_cubeCollector.transform.GetChild(_cubeCollector.transform.childCount - 1).gameObject);
                }
                else
                    Destroy(gameObject);
                
                transform.localScale -= new Vector3(0f, (float) _cubeSize, 0f);

            }

        }
        
    }
}
