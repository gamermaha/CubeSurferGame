﻿using Environment_Setters;
using Player_Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        
        public static GameManager Instance;
        
        [Header("Reference to Player Prefab")]
        [SerializeField] private PlayerController playerPrefab;
        
        private PlayerMovement _playerMovement;
        private Level _currentLevel;
        private PlayerController _playerController;
        private int _diamondCount;
        private int _levelNumber;
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
            _totalLevels = MetaData.Instance.scriptableInstance.noOfLevels;
            LoadCurrentLevel();
        }
        
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnLevelFinishLoading;
        }
        
        public void PlayerCanMoveNow() => PlayerMovement.StartMoving = true;

        public void PlayerMustStopNow() => PlayerMovement.StartMoving = false;
        
        public void LoadNextLevel()
        {
            _levelNumber++;
            LoadLevel(_levelNumber);
        }

        public void LoadCurrentLevel()
        {
            int levelToLoad = PlayerPrefs.GetInt("LevelSaved", 0);
            _levelNumber = levelToLoad;
            LoadLevel(_levelNumber);
        }
        
        public void GameOver()
        {
            AudioManager.Instance.PlaySounds(AudioManager.GAMEOVERSOUND);
            GameplayUIController.Instance.GameOverView(); 
        }
        
        public void LevelCompleted()
        {
            AudioManager.Instance.PlaySounds(AudioManager.GAMECOMPLETEDSOUND);
            GameplayUIController.Instance.EndLevelView();
        }
        
        public void AddDiamonds(int diamonds)
        {
            _diamondCount += diamonds;
            GameplayUIController.Instance.UpdateDiamondCount(_diamondCount);
        }
        private void OnLevelFinishLoading(Scene scene, LoadSceneMode mode)
        {
            if(scene.name != "SplashScreen")
                Init();
        }
        
        private void Init()
        {
            _playerController = Instantiate(playerPrefab);
            _playerMovement = _playerController.GetComponent<PlayerMovement>();
            _currentLevel = GetCurrentLevel();
            _playerMovement.SetStartPos(_currentLevel.StartPosition);
            _playerController.transform.position = _currentLevel.StartPosition.position;
            _playerMovement.PlayerPositions(_currentLevel.GiveWayPoints());
        }

        private Level GetCurrentLevel()
        {
            return FindObjectOfType<Level>();
        }
       
        private void LoadLevel(int levelID)
        {
            
            if (levelID != 0 && levelID <= _totalLevels)
            {
                AudioManager.Instance.PlaySounds(AudioManager.GAMESTARTSOUND);
                SceneManager.LoadScene("Level 0" + levelID);
                PlayerPrefs.SetInt("LevelSaved", _levelNumber);
            }
            else if (levelID > _totalLevels)
            {
                GameplayUIController.Instance.GameCompletedView();
                _levelNumber = 1;
                PlayerPrefs.SetInt("LevelSaved", _levelNumber);
            }
            else if (levelID == 0)
            {
                SceneManager.LoadScene("SplashScreen");
            }
        }
    }
}
