using System.Collections.Generic;
using Environment_Setters;
using Managers;
using UnityEngine;

namespace Player_Scripts
{ 
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameObject cubeCollector;
        [SerializeField] private GameObject playerCollider;
        [SerializeField] private Magnet magnetCollider;
        
        private PlayerMovement _playerManager;
        private Camera _cam;

        private List<GameObject> _cubesAdded;
        private List<Vector3> _addedCubePositions;
        private Vector3 _cubePos;
        private CubeToDestroy[] cubeToDestroyScripts;
        private double _cubeSize;
        private bool _isCubeDestroyed;
        
        private float _destroyMagnetTime;

        private float _timeForDiamondTimes2;
        private bool _isDiamondMulti;
        private int _diamondMultiplier;
        private int _incrementForObstacle;
        
        void Awake()
        {
            _playerManager = GetComponent<PlayerMovement>();
            _cam = GetComponentInChildren<Camera>();
            _cubesAdded = new List<GameObject>();
            _addedCubePositions = new List<Vector3>();
        }
        
        private void Start()
        {
            if (MetaData.Instance != null)
            {
                _cubeSize = MetaData.Instance.scriptableInstance.cubeLength;
                _timeForDiamondTimes2 = MetaData.Instance.scriptableInstance.diamondTimer;
                _destroyMagnetTime = MetaData.Instance.scriptableInstance.destroyMagnetTime;
            }
            _cubePos = Vector3.up * (float)_cubeSize/4;
            _cubesAdded.Add(cubeCollector.transform.GetChild(0).gameObject);
            _cubesAdded[0].gameObject.tag = Constants.TAG_CUBE;
        }

        private void Update()
        {
            if (_isDiamondMulti)
            {
                _timeForDiamondTimes2 -= Time.deltaTime;
                if (_timeForDiamondTimes2 <= 0)
                {
                    _isDiamondMulti = false;
                    GameplayUIController.Instance.DiamondAnimationTimesTwo("");
                }
            }
        }

        public void AddDiamond(GameObject collided)
        {
            if (!_isDiamondMulti)
                _diamondMultiplier = 1;
            else
                _diamondMultiplier = 2;
            
            AudioManager.Instance.PlaySounds(Constants.AUDIO_DIAMONDCOLLECTEDSOUND);
            GameManager.Instance.AddDiamonds(_diamondMultiplier);
            Vector3 screenPos = collided.transform.position;
            GameplayUIController.Instance.DiamondAnimation(screenPos, _cam);
            Destroy(collided);
        }
        
        public void AddCube(GameObject collided)
        {
            AudioManager.Instance.PlaySounds(Constants.AUDIO_CUBECOLLECTEDSOUND);
            collided.gameObject.tag = Constants.TAG_CUBEADDED;
            _playerManager.MoveUp(1);
            _cubesAdded.Add(collided);
            collided.transform.SetParent(cubeCollector.transform, false);
            collided.transform.localPosition = _cubePos;
            playerCollider.transform.localScale += new Vector3(0f, (float) _cubeSize, 0f);
            _cubePos += Vector3.up * (float) _cubeSize;
        }

        public void DestroyCube(GameObject collided, float obstacleSize, int incrementForObstacle)
        { 
            int _obstacleSize = (int) obstacleSize;
            _incrementForObstacle = incrementForObstacle;
            _isCubeDestroyed = true;
            
            if (_cubesAdded.Count > obstacleSize)
            {
                for (int o = 0; o < _obstacleSize; o++)
                {
                    AudioManager.Instance.PlaySounds(Constants.AUDIO_DESTROYCUBESOUND);
                    _cubesAdded[o].gameObject.tag = Constants.TAG_CUBEDESTROYED;
                    cubeCollector.transform.GetChild(0).SetParent(null);
                    _cubePos -= Vector3.up * (float) _cubeSize;
                } 
            }
            else
            {
                GameManager.Instance.GameOver();
                _playerManager.StopPlayer();
            }
            playerCollider.transform.localScale -= new Vector3(0f, (float) _cubeSize, 0f);
        }
        
        public void PullTrigger(Collider other)
        {
            if (other.CompareTag(Constants.TAG_DESTROYCUBE) && _isCubeDestroyed)
            {
                cubeToDestroyScripts = other.gameObject.GetComponentsInChildren<CubeToDestroy>();
                if (!_playerManager.wayPtFinished)
                    WaitToFall(cubeToDestroyScripts[_incrementForObstacle].obstacleSize);
            }
            else if (other.CompareTag(Constants.TAG_ENDLEVEL))
            {
                GameManager.Instance.LevelCompleted();
                _playerManager.StopPlayer();
            }
        }
        
        public void EndLadder(GameObject collided)
        {
            DestroyCube(collided,1, 0);
            playerCollider.transform.localScale -= new Vector3(0f, (float) _cubeSize, 0f);
            _cubesAdded.RemoveAt(0);
        }
        
        public void WaterObstacle()
        { 
            for (int i = 0; i < _cubesAdded.Count; i++)
                _addedCubePositions.Add(_cubesAdded[i].transform.position);
            
            if (_cubesAdded.Count <= 0)
            {
                GameManager.Instance.GameOver();
                _playerManager.StopPlayer();
            }
            else
            {
                Destroy(_cubesAdded[0]);
                AudioManager.Instance.PlaySounds(Constants.AUDIO_DESTROYCUBESOUND);
                playerCollider.transform.localScale -= new Vector3(0f, (float) _cubeSize, 0f);
            
                for (int k = 1; k < _cubesAdded.Count; k++)
                {
                    var pos = _cubesAdded[k].transform.position;
                    _cubesAdded[k].transform.position = new Vector3(pos.x, _addedCubePositions[k-1].y, pos.z);
                }
                _cubesAdded.RemoveAt(0);
                _addedCubePositions.Clear();
                _cubePos -= Vector3.up * (float) _cubeSize;
                _playerManager.MoveDown(1);
            }
            
            
            
        }

        public void MagnetCollected(GameObject magnet)
        {
            AudioManager.Instance.PlaySounds(Constants.AUDIO_MAGNETCOLLECTEDSOUND);
            magnet.tag = Constants.TAG_MAGNETGRABBED;
            Magnet magnetCol = Instantiate(magnetCollider);
            magnetCol.transform.SetParent(transform.GetChild(0));
            magnet.transform.SetParent(transform.GetChild(0));
            magnet.transform.localPosition = new Vector3(0.5f, 2f, -2f);
            Destroy(magnetCol, _destroyMagnetTime);
            Destroy(magnet, _destroyMagnetTime);
        }

        public void DiamondMulti(GameObject diamondMultiplier)
        {
            AudioManager.Instance.PlaySounds(Constants.AUDIO_DIAMONDMULTIPLIERSOUND);
            _isDiamondMulti = true;
            Destroy(diamondMultiplier);
            GameplayUIController.Instance.DiamondAnimationTimesTwo("X2");
        }
        
        private void WaitToFall(float obstacleSize)
        {
            int _obstacleSize = (int) obstacleSize;
            _playerManager.MoveDown(_obstacleSize);
            
            for (int i = 0; i < _cubesAdded.Count; i++)
                _addedCubePositions.Add(_cubesAdded[i].transform.position);
            
            for (int k = _obstacleSize; k < _cubesAdded.Count; k++)
            {
                var pos = _cubesAdded[k].transform.position;
                _cubesAdded[k].transform.position = new Vector3(pos.x, _addedCubePositions[k-_obstacleSize].y, pos.z);
            }
            _cubesAdded.RemoveRange(0,_obstacleSize);
            _addedCubePositions.Clear();
            _isCubeDestroyed = false;
        }
    }
}

