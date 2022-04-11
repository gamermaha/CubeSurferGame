using System;
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
        public int levelNumber;
        
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
        
        
        private int _totalLevels;

       
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnLevelFinishLoading;
        }
        void Start()
        {
            Debug.Log($"");
            _pathLength = MetaData.Instance.scriptableInstance.pathLength;
            _totalLevels = MetaData.Instance.scriptableInstance.noOflevels;
            uIController = FindObjectOfType<GameplayUIController>();
            levelNumber = 1;
            SceneManager.LoadScene("Level 01");
            //slider.value = 0;
            
        }

        private void Update()
        {
            // if (_player.endIsReached)
            //     uIController.EndGame();
            // if (_player.gameIsOver)
            //     uIController.GameOver();
            
            //slider.value = _inputManager.lengthCoveredPercentage;
            //Debug.Log(slider.value);
        }
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnLevelFinishLoading;
        }

        private void OnLevelFinishLoading(Scene scene, LoadSceneMode mode)
        {
            Debug.Log(scene.name);
            Debug.Log("Level 0" + levelNumber);
            
            if (scene.name == "Level 0" + levelNumber)
            {
                Init();
            }
            
        }

        // private void PlayerSetup()
        // {
        //     //// Dynamic WayPoint/Path Implementation
        //     //_playerXValue = _path.transform.position.x - _pathLength/2 + player.transform.localScale.x;
        //     //_playerYValue = _path.transform.position.y + player.transform.localScale.y;
        //     //_playerZValue = _path.transform.position.z + player.transform.localScale.z;
        //     
        //     _playerXValue = LevelDecider().transform.position.x;
        //     _playerYValue = LevelDecider().transform.position.y + LevelDecider().transform.localScale.y/2 + player.transform.localScale.y/2 + 1.25f;
        //     _playerZValue = LevelDecider().transform.position.z - 19f + player.transform.localScale.z/2;
        //     //Debug.Log(_playerYValue);
        // }
        private void Init()
        {
            //// Dynamic WayPoint/Path Implementation
            //_path = Instantiate(path, new Vector3(0, 0, 0), Quaternion.identity);
            //PlayerSetup();
            _player = Instantiate(player);
            //LevelDecider().StartPosition.position, Quaternion.identity);
            _inputManager = _player.GetComponent<InputClass>();
            _levelTBD = LevelDecider();
            _inputManager.PlayerPositions(_levelTBD.GiveWayPoints());
            _player.transform.position = _levelTBD.StartPosition.position;
            //Debug.Log(_player.transform.position);
            

        }

        private Level LevelDecider()
        {
            return FindObjectOfType<Level>();
        }

        public void GameOverCall()
        {
            uIController.GameOver(); 
        }

        public void EndGameCall()
        {
            uIController.EndGame();
        }

        public void LoadNewLevel()
        {
            SceneManager.LoadScene("Level 0" + levelNumber); 
        }
    }
    
}
