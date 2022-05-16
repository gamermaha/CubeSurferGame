﻿using System.Collections.Generic;
using DG.Tweening;
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
        private float _playerSpeed;
        private Camera _cam;
        
        private List<GameObject> _cubesAdded;
        private List<Vector3> _addedCubePositions;
        private Vector3 _cubePos;
        private CubeToDestroy[] _cubeToDestroyScripts;
        private double _cubeSize;
        private bool _isCubeDestroyed;
        
        private float _destroyMagnetTime;
        
        private float _timeForDiamondTimes2;
        private bool _isDiamondMulti;
        private int _diamondsToBeAdded;
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
                _playerSpeed = MetaData.Instance.scriptableInstance.playerSpeed;
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
                    MenuManager.Instance.CallDiamondAnimationTimesTwo("");
                }
            }
        }
        
        public void AddDiamond(GameObject collided)
        {
            _diamondsToBeAdded = _isDiamondMulti ? 2 : 1;
            
            AudioManager.Instance.PlaySounds(Constants.AUDIO_DIAMONDCOLLECTEDSOUND);
            GameManager.Instance.AddDiamonds(_diamondsToBeAdded);
            Vector3 screenPos = collided.transform.position;
            MenuManager.Instance.CallDiamondAnimation(screenPos, _cam);
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
        }
        
        public void PullTrigger(Collider collided)
        {
            if (collided.CompareTag(Constants.TAG_DESTROYCUBE) && _isCubeDestroyed)
            {
                _cubeToDestroyScripts = collided.gameObject.GetComponentsInChildren<CubeToDestroy>();
                if (!_playerManager.wayPtFinished)
                    WaitToFall(_cubeToDestroyScripts[_incrementForObstacle].obstacleSize);
            }
            else if (collided.CompareTag(Constants.TAG_ENDLEVEL))
            {
                OnLevelComplete();
            }
        }
        
        public void EndLadder(GameObject collided, float camMovement)
        {
            
            if (_cubesAdded.Count > 1)
            {
                _cam.transform.DOMove(new Vector3(_cam.transform.position.x, _cam.transform.position.y + camMovement, _cam.transform.position.z), 1/_playerSpeed);
                    // .SetEase(Ease.InOutFlash).OnComplete(() =>
                    // {
                    // });
                //_cam.transform.position += new Vector3(0f, camMovement, 0f);
                DestroyCube(collided, 1, 0);
                _cubesAdded.RemoveAt(0);
            }
            else
            {
                OnLevelComplete();
            }
        }
        
        public void WaterObstacle()
        {
            if (_cubesAdded.Count > 1)
            {
                Destroy(_cubesAdded[0]);
                AudioManager.Instance.PlaySounds(Constants.AUDIO_DESTROYCUBESOUND);

                MoveCubesDown(1);
                _cubePos -= Vector3.up * (float) _cubeSize;
                _playerManager.MoveDown(1);
            }
            else
            {
                GameManager.Instance.GameOver();
                _playerManager.StopPlayer();
            }
        }

        public void MagnetCollected(GameObject magnet)
        {
            AudioManager.Instance.PlaySounds(Constants.AUDIO_MAGNETCOLLECTEDSOUND);
            magnet.tag = Constants.TAG_MAGNETGRABBED;
            Magnet magnetCol = Instantiate(magnetCollider);
            magnetCol.transform.SetParent(transform.GetChild(0), false);
            
            magnet.transform.SetParent(transform.GetChild(0));
            magnet.transform.localScale = new Vector3(15f, 15f, 15f);
            magnet.transform.localPosition = new Vector3(0.4f, -0.2f, -2f);
            magnet.transform.localRotation = Quaternion.Euler(0f, 50f, -50f);

            Destroy(magnetCol, _destroyMagnetTime);
            Destroy(magnet, _destroyMagnetTime);
        }

        public void DiamondMulti(GameObject diamondMultiplier)
        {
            AudioManager.Instance.PlaySounds(Constants.AUDIO_DIAMONDMULTIPLIERSOUND);
            _isDiamondMulti = true;
            Destroy(diamondMultiplier);
            MenuManager.Instance.CallDiamondAnimationTimesTwo("X2");
        }

        private void WaitToFall(float obstacleSize)
        {
            int _obstacleSize = (int) obstacleSize;
            _playerManager.MoveDown(_obstacleSize);
            MoveCubesDown(_obstacleSize);
            _isCubeDestroyed = false;
        }

        private void MoveCubesDown(float obstacleSize)
        {
            int _obstacleSize = (int) obstacleSize;
            
            for (int i = 0; i < _cubesAdded.Count; i++)
                _addedCubePositions.Add(_cubesAdded[i].transform.position);
            
            for (int k = _obstacleSize; k < _cubesAdded.Count; k++)
            {
                var pos = _cubesAdded[k].transform.position;
                _cubesAdded[k].transform.position = new Vector3(pos.x, _addedCubePositions[k-_obstacleSize].y, pos.z);
            }
            _cubesAdded.RemoveRange(0,_obstacleSize);
            _addedCubePositions.Clear();
        }

        private void OnLevelComplete()
        {
            MenuManager.Instance.CallShowConfetti(new Vector3(transform.position.x + 2f,transform.position.y + 30f, transform.position.z));
            GameManager.Instance.LevelCompleted();
            _playerManager.StopPlayer();
        }
    }
}

