
using System.Collections.Generic;
using Controllers;
using Managers;
using UnityEngine;

namespace Player_Scripts
{
    public class PlayerController : MonoBehaviour
    {

        public bool diamondMulti = false;
        public GameObject magnetSprite;
        
        [SerializeField] private GameObject cubeCollector;
        [SerializeField] private GameObject diamondCollector;
        [SerializeField] private GameObject playerCollider;
        
        private PlayerMovement _playerManager;
        private Canvas _myCanvas;
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
        private Camera _cam;
        
        private int _wayPtIncrement;
        private float _xValue;
        private float _yValue;
        private float _zValue;
        private double _cubeSize;
        private int _loopDiamond;
        private float _timeForDiamondTimes2;

        private float _timeToDrop;
        
        void Awake()
        {
            _playerManager = GetComponent<PlayerMovement>();
            _cam = GetComponentInChildren<Camera>();
            _cubes = new List<GameObject>();
            _cubePositions = new List<Vector3>();
            
        }
        private void Start()
        {
            if (MetaData.Instance != null)
            {
                _cubeSize = MetaData.Instance.scriptableInstance.cubeLength;
                _timeForDiamondTimes2 = MetaData.Instance.scriptableInstance.diamondTimer;
            }
            _cubePos = Vector3.up * (float)_cubeSize/4;
            _cubes.Add(cubeCollector.transform.GetChild(0).gameObject);
            _cubes[0].gameObject.tag = "Cube";
        }

        private void Update()
        {
            if (magnetSprite != null)
            {
                magnetSprite.transform.localPosition = new Vector3(cubeCollector.transform.GetChild(0).position.x, cubeCollector.transform.GetChild(0).position.y + 2f, cubeCollector.transform.GetChild(0).position.z);
            }

            if (diamondMulti)
            {
                _timeForDiamondTimes2 -= Time.deltaTime;
                if (_timeForDiamondTimes2 <= 0)
                {
                    diamondMulti = false;
                    DiamondMultiAnimation("");
                }
            }
        }

        public void AddDiamond(GameObject collided)
        {
            if (!diamondMulti)
            {
                _loopDiamond = 1;
            }
            else
            {
                _loopDiamond = 2;
            }
            collided.transform.SetParent(diamondCollector.transform, false);
            collided.gameObject.tag = "DiamondAdded";

            for (int i = 1; i <= _loopDiamond; i++)
            {
                GameManager.Instance.DiamondCountUpdate();
                Vector3 screenPos = collided.transform.position;
                GameplayUIController.Instance.DiamondAnimation(screenPos, _cam);
            }
            collided.SetActive(false);
            
        }
        public void AddCube(GameObject collided)
        {
            collided.gameObject.tag = "CubeAdded";
            _playerManager.MoveUp(1);
            _cubes.Add(collided);
            collided.transform.SetParent(cubeCollector.transform, false);
            collided.transform.localPosition = _cubePos;
            playerCollider.transform.localScale += new Vector3(0f, (float) _cubeSize, 0f);
            _cubePos += Vector3.up * (float) _cubeSize;
            
        }

        public void DestroyCube(GameObject collided, float obstacleSize)
        { 
            int _obstacleSize = (int) obstacleSize;

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
                GameManager.Instance.GameOverCall();
                _playerManager.StopPlayer();
            }
            playerCollider.transform.localScale -= new Vector3(0f, (float) _cubeSize, 0f);
        }
        public void PullTrigger(Collider other)
        {
            if (other.CompareTag("CubeDestroy") && PlayerCollider.DestroyCubeCalled)
            {
                cubeToDestroyScripts = other.gameObject.GetComponentsInChildren<CubeToDestroy>();
                if (!_playerManager.wayPtFinished)
                {
                    Vector3 playerLocalPos = transform.GetChild(0).localPosition;
                    Debug.Log(transform.GetChild(0).localPosition);
                    int increment = 0;
                    if (cubeToDestroyScripts.Length == 3)
                    {
                        if (playerLocalPos.x >= -3f && playerLocalPos.x < -1f)
                        {
                            increment = 0;
                            
                        }
                        else if (playerLocalPos.x >= -1f && playerLocalPos.x <= 1f)
                        {
                            increment = 1;
                            
                        }
                        else if (playerLocalPos.x > 1f && playerLocalPos.x <= 3f)
                        {
                            increment = 2;
                        }
                     
                    }
                    else
                    {
                        increment = 0;
                    }
                    WaitToFall(cubeToDestroyScripts[increment].obstacleSize);
                    
                }
            }
            if (other.CompareTag("EndLevel"))
            {
                GameManager.Instance.EndGameCall();
                _playerManager.StopPlayer();
            }
            
        }
        private void WaitToFall(float obstacleSize)
        {
            int _obstacleSize = (int) obstacleSize;
            
            _playerManager.MoveDown(_obstacleSize);
            
            for (int i = 0; i < _cubes.Count; i++)
            { 
                _cubePositions.Add(_cubes[i].transform.position);
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

        public void EndLadder(GameObject collided)
        {
            DestroyCube(collided,1);
            playerCollider.transform.localScale -= new Vector3(0f, (float) _cubeSize, 0f);
            _cubes.RemoveAt(0);
            
        }

        public void EndLevel(GameObject endlevel)
        {
            DestroyCube(endlevel.gameObject, 1f);
            playerCollider.transform.localScale -= new Vector3(0f, (float) _cubeSize, 0f);
            _cubes.RemoveAt(0);
        }

        public void WaterObstacle()
        { 
            for (int i = 0; i < _cubes.Count; i++)
            { 
                _cubePositions.Add(_cubes[i].transform.position);
            }
            Destroy(_cubes[0]);
            playerCollider.transform.localScale -= new Vector3(0f, (float) _cubeSize, 0f);
            for (int k = 1; k < _cubes.Count; k++)
            {
                var pos = _cubes[k].transform.position;
                _cubes[k].transform.position = new Vector3(pos.x, _cubePositions[k-1].y, pos.z);
            }
            _cubes.RemoveAt(0);
            
            _cubePositions.Clear();
            _cubePos -= Vector3.up * (float) _cubeSize;
            _playerManager.MoveDown(1);

            if (_cubes.Count <= 0)
            {
                GameManager.Instance.GameOverCall();
                _playerManager.StopPlayer();
            }
        }

        public void Magnet()
        {
            
        }

        public void DiamondMultiAnimation(string display)
        {
            GameplayUIController.Instance.DiamondAnimationTimesTwo(display);
        }
        
        
    }
}

