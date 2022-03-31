
using System.Collections.Generic;
using Controllers;
using Managers;
using UnityEngine;

namespace Player_Scripts
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private GameObject cubeCollector;
        [SerializeField] private GameObject destroyedCubeCollector;
        [SerializeField] private PlayerCollider playerCollider;

        private InputClass inputManager;
        
        private List<Transform> _playerPositions;
        private List<GameObject> _cubes = new List<GameObject>();
        private List<Vector3> _cubePositions = new List<Vector3>();
        
        private Vector3 _cubePos;
        private Vector3 _prevMousePos;
        private Vector3 _prevPlayerPos;
        private Vector3 _cubeStopPos;
        
        private int _wayPtIncrement;
        private float _mySpeed;
        private float _moveForce;
        private float _xValue;
        private float _yValue;
        private float _zValue;
        private double _cubeSize;
        
        

       

        void Awake()
        {
            inputManager = GetComponent<InputClass>();
        }
        private void Start()
        {
            if (MetaData.Instance == null)
            {
                _mySpeed = 0;
                _moveForce = 0;
            }
            else
            {
                _mySpeed = MetaData.Instance.scriptableInstance.playerSpeed;
                _moveForce = MetaData.Instance.scriptableInstance.playerForce;
            }
            
            _cubeSize = MetaData.Instance.scriptableInstance.cubeLength;
            _cubePos = Vector3.up * (float)_cubeSize/4;
            _cubes.Add(cubeCollector.transform.GetChild(0).gameObject);
            
        }

        void Update()
        {
            if (_cubes.Count == 0)
            {
                inputManager.StopPlayer();
            }
        }


        public void AddCube(GameObject collided)
        {
            inputManager.MoveUp();
            _cubes.Add(collided);
            Debug.Log("I have encountered a cube to be added");
            collided.transform.SetParent(cubeCollector.transform, false);
            collided.transform.localPosition = _cubePos;
            _cubePos += Vector3.up * (float) _cubeSize;
            collided.gameObject.tag = "CubeAdded";
            
        }

        public void DestroyCube(GameObject collided)
        {
            inputManager.MoveDown();
            _cubePos -= Vector3.up * (float) _cubeSize;
            Debug.Log("I have encountered a cube to be destroyed");
            
            if (_cubes.Count > 0)
            {
                _cubes[0].gameObject.tag = "CubeDestroyed";
                _cubeStopPos = _cubes[0].gameObject.transform.position;
                Debug.Log(_cubes[0].gameObject.name);
                //stop cube
                //var transformParent = _cubes[0].gameObject.transform;
                cubeCollector.transform.GetChild(0).SetParent(null);
                
                //Destroy(_cubes[_cubes.Count - 1].gameObject);

                for (int i = 0; i < _cubes.Count; i++)
                { 
                    _cubePositions.Add(_cubes[i].transform.position);
                }

                for (int k = 1; k < _cubes.Count; k++)
                {
                    _cubes[k].transform.position = _cubePositions[k-1];
                }
                _cubes[0].gameObject.transform.position = _cubeStopPos;
                _cubes.RemoveAt(0);
                _cubePositions.Clear();
            }
        }
        
    }
}
