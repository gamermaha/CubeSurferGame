

using System.Collections.Generic;
using Controllers;
using Managers;
using UnityEngine;

namespace Player_Scripts
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject cubeCollector;
        [SerializeField] private GameObject destroyedCubeCollector;
        //[SerializeField] private PlayerCollider playerCollider;

        [SerializeField] private GamePlayUIController uiController;
        //[SerializeField] private Animator anim;

        private InputClass inputManager;
        
        private List<Transform> _playerPositions;
        private List<GameObject> _cubes = new List<GameObject>();
        private List<Vector3> _cubePositions = new List<Vector3>();
        private Transform[] _cubeChildren;
        
        private Vector3 _cubePos;
        private Vector3 _prevMousePos;
        private Vector3 _prevPlayerPos;
        private Vector3 _cubeStopPos;
        private Vector3 _playerPosAtCol;
        
        private int _wayPtIncrement;
        private float _mySpeed;
        private float _moveForce;
        private float _xValue;
        private float _yValue;
        private float _zValue;
        private double _cubeSize;

        private float _timeToDrop;
        public bool endIsReached;
        public bool gameIsOver;

        private Animator anim;

        private string upDown = "Up";
        //private float _obstacleNumber;
        
        

       

        void Awake()
        {
            inputManager = GetComponent<InputClass>();
            //anim = player.GetComponentInChildren<Animator>();
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
            _cubes[0].gameObject.tag = "Cube";

        }
        public void AddCube(GameObject collided)
        {
            inputManager.MoveUp(1);
            _cubes.Add(collided);
            Debug.Log("I have encountered a cube to be added");
            collided.transform.SetParent(cubeCollector.transform, false);
            collided.transform.localPosition = _cubePos;
            _cubePos += Vector3.up * (float) _cubeSize;
            collided.gameObject.tag = "CubeAdded";
            //anim.SetBool(upDown, false);
            
        }

        public void DestroyCube(GameObject collided, float obstacleSize)
        {
            
            Debug.Log("I have encountered a cube to be destroyed");
            int _obstacleSize = (int) obstacleSize;
            //_playerPosAtCol = transform.position;
            
            if (_cubes.Count > obstacleSize)
            {
                for (int o = 0; o < _obstacleSize; o++)
                {
                    _cubes[o].gameObject.tag = "CubeDestroyed";
                    cubeCollector.transform.GetChild(0).SetParent(null);
                    _cubePos -= Vector3.up * (float) _cubeSize;
                } 
            }
            else
            {
                gameIsOver = true;
                inputManager.StopPlayer();
            }
            
        }

        private void WaitToFall(float obstacleSize)
        {
            int _obstacleSize = (int) obstacleSize;
            
            inputManager.MoveDown(_obstacleSize);
            
            for (int i = 0; i < _cubes.Count; i++)
            { 
                _cubePositions.Add(_cubes[i].transform.position);
            }
            for (int k = _obstacleSize; k < _cubes.Count; k++)
            {
                //Debug.Log($"Old Pos: {_cubes[k].transform.position} New {_cubePositions[k-_obstacleSize].y}");
                var pos = _cubes[k].transform.position;
                _cubes[k].transform.position = new Vector3(pos.x, _cubePositions[k-_obstacleSize].y, pos.z);
            }
            for (int o = 0; o < _obstacleSize; o++)
            {
                _cubes.RemoveAt(0);
            }
            _cubePositions.Clear();
            PlayerCollider.DestroyCubeCalled = false;
            //anim.SetTrigger(upDown, false);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("OnTriggerEnter " + other.gameObject.tag);
            if (other.CompareTag("EndLevel"))
            {
                endIsReached = true;
                Debug.Log("End level is reached");
                inputManager.StopPlayer();
                
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("CubeDestroy") && PlayerCollider.DestroyCubeCalled)
            {
                WaitToFall(other.gameObject.GetComponent<CubeToDestroy>().obstacleSize);
            }
        }
        
    }
}

