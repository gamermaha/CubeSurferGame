


using System.Collections.Generic;

using Controllers;

using Managers;
using UnityEngine;



namespace Player_Scripts
{
    public class PlayerController : MonoBehaviour
    {

        public bool diamondMulti = false;
        public bool magnetEnabled = false;
        public GameObject magnetSprite;
        [SerializeField] private GameObject cubeCollector;
        [SerializeField] private GameObject diamondCollector;
        [SerializeField] private GameObject animatedDiamond;
        [SerializeField] private Vector3 targetPositionForDiamond;
        [SerializeField] private GameObject playerCollider;
        [SerializeField] private Magnet magnet; 
        

        [SerializeField] [Range(0.5f, 0.9f)] private float minAnimDuration;
        [SerializeField] [Range(0.9f, 2f)] private float maxAnimDuration;
        
        
        
        private InputClass _inputManager;
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

        private float _timeToDrop;
        
        void Awake()
        {
            _inputManager = GetComponent<InputClass>();
            _cam = GetComponentInChildren<Camera>();
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
            //_myCanvas = FindObjectOfType<Canvas>();

        }

        private void Update()
        {
            if (magnetSprite != null)
            {
                Debug.Log("Cube[0] animation pos is " + cubeCollector.transform.GetChild(0).position);
                magnetSprite.transform.localPosition = new Vector3(cubeCollector.transform.GetChild(0).position.x, cubeCollector.transform.GetChild(0).position.y + 2f, cubeCollector.transform.GetChild(0).position.z);
                Debug.Log("Magnet animation pos is " + magnetSprite.transform.position);
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
            _inputManager.MoveUp(1);
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
                    //Debug.Log(cubeCollector.transform.GetChild(0).name);
                    cubeCollector.transform.GetChild(0).SetParent(null);
                    _cubePos -= Vector3.up * (float) _cubeSize;
                } 
            }
            else
            {
                GameManager.Instance.GameOverCall();
                _inputManager.StopPlayer();
            }
            playerCollider.transform.localScale -= new Vector3(0f, (float) _cubeSize, 0f);
        }
        public void PullTrigger(Collider other)
        {
            if (other.CompareTag("CubeDestroy") && PlayerCollider.DestroyCubeCalled)
            {
                cubeToDestroyScripts = other.gameObject.GetComponentsInChildren<CubeToDestroy>();
                if (!_inputManager.wayPtFinished)
                {
                    Vector3 playerLocalPos = transform.GetChild(0).localPosition;
                    Debug.Log(transform.GetChild(0).localPosition);
                
                    if (cubeToDestroyScripts.Length == 3)
                    {
                        if (playerLocalPos.x >= -3f && playerLocalPos.x < -1f)
                        {
                            WaitToFall(cubeToDestroyScripts[0].obstacleSize);
                        }
                        else if (playerLocalPos.x >= -1f && playerLocalPos.x <= 1f)
                        {
                            WaitToFall(cubeToDestroyScripts[1].obstacleSize);
                        }
                        else if (playerLocalPos.x > 1f && playerLocalPos.x <= 3f)
                        {
                            WaitToFall(cubeToDestroyScripts[2].obstacleSize);
                        }
                     
                    }
                    else
                    {
                        WaitToFall(cubeToDestroyScripts[0].obstacleSize);
                    }
                    
                }
            }
            if (other.CompareTag("EndLevel"))
            {
                GameManager.Instance.EndGameCall();
                //Debug.Log("End level is reached");
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
                //Debug.Log(_cubePositions[i]);
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
            //_inputManager.MoveUp(1);
        }

        public void EndLevel(GameObject endlevel)
        {
            DestroyCube(endlevel.gameObject, 1f);
            playerCollider.transform.localScale -= new Vector3(0f, (float) _cubeSize, 0f);
            _cubes.RemoveAt(0);
        }

        public void WaterObstacle()
        { Debug.Log("Cube count is "+ _cubes.Count);
            Debug.Log("Water Obstacle encountered");
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
            _inputManager.MoveDown(1);
            Debug.Log("Cube count is "+ _cubes.Count);

            if (_cubes.Count <= 0)
            {
                GameManager.Instance.GameOverCall();
                _inputManager.StopPlayer();
            }
        }

        public void DiamondMultiAnimation(Vector3 diamondMultiPos)
        {
            GameplayUIController.Instance.DiamondAnimationTimesTwo();
        }
        
        
    }
}

