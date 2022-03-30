using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.Serialization;


namespace Player_Scripts
{
    public class PlayerCollider : MonoBehaviour
    {
        public static bool AddCube;
        public static bool DestroyCube;

       [SerializeField] private GameObject cubeCollector;

        private List<GameObject> _cubes = new List<GameObject>();

        private Vector3 _newPos;
        private Vector3 _cubePos;
        private double _cubeSize;
        
        


        private void Start()
        {
            _cubeSize = MetaData.Instance.scriptableInstance.cubeLength;
            _newPos = Vector3.up * (float)_cubeSize/4;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Cube"))
            {
                
                _cubes.Add(other.gameObject);
                Debug.Log("I have encountered a cube to be added");
                
                AddCube = true;
                //Debug.Log(transform.position);
                
                other.transform.SetParent(cubeCollector.transform, false);
                
                other.transform.localPosition = _newPos;
                _newPos += Vector3.up * (float) _cubeSize;
                

                other.gameObject.tag = "CubeAdded";

                transform.localScale += new Vector3(0f, (float) _cubeSize, 0f);
                
            }
            if (other.gameObject.CompareTag("CubeDestroy"))
            {
                _newPos -= Vector3.up * (float) _cubeSize;
               
                if (_cubes.Count > 0)
                {
                    DestroyCube = true;
                    
                    Debug.Log("I have encountered a cube to be destroyed");
                    Debug.Log(_cubes.Count);
                    Destroy(_cubes[_cubes.Count - 1].gameObject);
                    _cubes.RemoveAt(_cubes.Count-1);
                }
                else
                    Destroy(gameObject);
                
                transform.localScale -= new Vector3(0f, (float) _cubeSize, 0f);
            }

        }
    }
}
