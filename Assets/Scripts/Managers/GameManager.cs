﻿using System;
using Controllers;
using Environment_Setters;
using Player_Scripts;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        
        public static GameManager Instance;
        
        [Header(" GameObjects Imported")]
        [SerializeField] private PlayerController player;
        [SerializeField] private Path path;
        [SerializeField] private Level level01;
        [SerializeField] private GameplayUIController uIController;
        [SerializeField] private Slider slider;


        private InputClass _inputManager;
        private Level _levelTBD;
        private Path _path;
        private PlayerController _player;
        
        private float _pathLength;
        private float _playerXValue;
        private float _playerYValue;
        private float _playerZValue;
        
        private int x = 1;

       
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            slider.value = 0;
        }
        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnLevelFinishLoading;
        }
        void Start()
        {
            _pathLength = MetaData.Instance.scriptableInstance.pathLength;
            slider.value = 0;
        }

        private void Update()
        {
            if (_player.endIsReached)
                uIController.EndGame();
            if (_player.gameIsOver)
                uIController.GameOver();
            
            slider.value = _inputManager.lengthCoveredPercentage;
            //Debug.Log(slider.value);
        }
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnLevelFinishLoading;
        }

        private void OnLevelFinishLoading(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "Level 03")
            {
                Init();
            }
        }

        private void PlayerSetup()
        {
            //// Dynamic WayPoint/Path Implementation
            //_playerXValue = _path.transform.position.x - _pathLength/2 + player.transform.localScale.x;
            //_playerYValue = _path.transform.position.y + player.transform.localScale.y;
            //_playerZValue = _path.transform.position.z + player.transform.localScale.z;
            
            _playerXValue = LevelDecider().transform.position.x;
            _playerYValue = LevelDecider().transform.position.y + LevelDecider().transform.localScale.y/2 + player.transform.localScale.y/2 + 1.25f;
            _playerZValue = LevelDecider().transform.position.z - 19f + player.transform.localScale.z/2;
            //Debug.Log(_playerYValue);
        }
        private void Init()
        {
            //// Dynamic WayPoint/Path Implementation
            //_path = Instantiate(path, new Vector3(0, 0, 0), Quaternion.identity);
            //PlayerSetup();
            _player = Instantiate(player);
            //LevelDecider().StartPosition.position, Quaternion.identity);
            _inputManager = _player.GetComponent<InputClass>();
            
            _inputManager.PlayerPositions(level01.GiveWayPoints());
            _player.transform.position = LevelDecider().StartPosition.position;
            //Debug.Log(_player.transform.position);
            

        }

        private Level LevelDecider()
        {
            _levelTBD = level01;
            return _levelTBD;
        }
    }
    
}
