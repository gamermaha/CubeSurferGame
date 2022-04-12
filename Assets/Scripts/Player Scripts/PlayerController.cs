

using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using Managers;
using UnityEngine;

namespace Player_Scripts
{
    public class PlayerController : MonoBehaviour
    {
        
        [SerializeField] private GameObject cubeCollector;
        
        private InputClass _inputManager;
        
        private List<Transform> _playerPositions;
        private List<GameObject> _cubes;
        private List<Vector3> _cubePositions;
        private Transform[] _cubeChildren;
        private CubeToDestroy[] cubeToDestroyScripts;
        
        private Vector3 _cubePos;
        private Vector3 _prevMousePos;
        private Vector3 _prevPlayerPos;
        private Vector3 _cubeStopPos;
        private Vector3 _playerPosAtCol;
        
        private int _wayPtIncrement;
        private float _xValue;
        private float _yValue;
        private float _zValue;
        private double _cubeSize;

        private float _timeToDrop;
        
        void Awake()
        {
            _inputManager = GetComponent<InputClass>();
            _cubes = new List<GameObject>();
            _cubePositions = new List<Vector3>();
        }
        private void Start()
        {
            if (MetaData.Instance != null)
            {
                _cubeSize = MetaData.Instance.scriptableInstance.cubeLength;
            }
            _cubePos = Vector3.up * (float)_cubeSize/4;
            _cubes.Add(cubeCollector.transform.GetChild(0).gameObject);
            _cubes[0].gameObject.tag = "Cube";

        }

        public void AddDiamond(GameObject collided)
        {
            collided.transform.SetParent(cubeCollector.transform, false);
            collided.gameObject.tag = "DiamondAdded";
            collided.SetActive(false);
            GameplayUIController.Instance.DiamondCountIncrement();
        }
        public void AddCube(GameObject collided)
        {
            _inputManager.MoveUp(1);
            _cubes.Add(collided);
            collided.transform.SetParent(cubeCollector.transform, false);
            collided.transform.localPosition = _cubePos;
            _cubePos += Vector3.up * (float) _cubeSize;
            collided.gameObject.tag = "CubeAdded";
        }

        public void DestroyCube(GameObject collided, float obstacleSize)
        { 
            int _obstacleSize = (int) obstacleSize;

            if (_cubes.Count > obstacleSize)
            {
                for (int o = 0; o < _obstacleSize; o++)
                {
                    _cubes[o].gameObject.tag = "CubeDestroyed";
                    Debug.Log(cubeCollector.transform.GetChild(0).name);
                    cubeCollector.transform.GetChild(0).SetParent(null);
                    _cubePos -= Vector3.up * (float) _cubeSize;
                } 
            }
            else
            {
                GameManager.Instance.GameOverCall();
                _inputManager.StopPlayer();
            }
            
        }
        public void PullTrigger(Collider other)
        {
            if (other.CompareTag("CubeDestroy") && PlayerCollider.DestroyCubeCalled)
            {
                cubeToDestroyScripts = other.gameObject.GetComponentsInChildren<CubeToDestroy>();
                if (!_inputManager.wayPtFinished)
                {
                    WaitToFall(cubeToDestroyScripts[0].obstacleSize);
                }
            }
            if (other.CompareTag("EndLevel"))
            {
                DestroyCube(other.gameObject, 1f);
                GameManager.Instance.EndGameCall();
                Debug.Log("End level is reached");
                _inputManager.StopPlayer();
            }
            
        }
        private void WaitToFall(float obstacleSize)
        {
            int _obstacleSize = (int) obstacleSize;
            
            _inputManager.MoveDown(_obstacleSize);
            
            for (int i = 0; i < _cubes.Count; i++)
            { 
                _cubePositions.Add(_cubes[i].transform.position);
                Debug.Log(_cubePositions[i]);
            }
            for (int k = _obstacleSize; k < _cubes.Count; k++)
            {
                var pos = _cubes[k].transform.position;
                _cubes[k].transform.position = new Vector3(pos.x, _cubePositions[k-_obstacleSize].y, pos.z);
            }
            for (int o = 0; o < _obstacleSize; o++)
            {
                _cubes.RemoveAt(0);
            }
            _cubePositions.Clear();
            PlayerCollider.DestroyCubeCalled = false;
            
        }
        
    }
}

