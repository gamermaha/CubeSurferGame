﻿using Environment_Setters;
using Player_Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        
        public static GameManager Instance;
        public bool playerPaused;
        
        [Header("Reference to Player Prefab")]
        [SerializeField] private PlayerController playerPrefab;
        
        private PlayerMovement _playerMovement;
        private PlayerController _playerController;
        private Level _currentLevel;
        private int _totalDiamondCount;
        private int _diamondCountAtEachLevel;
        private int _levelNumber;
        private int _totalLevels;
        private bool _isOnStart;
        private int _audioOnOff;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
                Destroy(gameObject);
        }
        
        private void OnEnable() => SceneManager.sceneLoaded += OnLevelFinishLoading;

        void Start()
        {
            _totalLevels = MetaData.Instance.scriptableInstance.noOfLevels;
            _isOnStart = true;
            LoadCurrentLevel();
            PlayerPrefs.SetInt("audio", 1);
            AudioManager.Instance.GetOnOff();
        }
        
        private void OnDisable() => SceneManager.sceneLoaded -= OnLevelFinishLoading;

        public void PlayerCanMoveNow() => PlayerMovement.StartMoving = true;

        public void PlayerMustStopNow() => PlayerMovement.StartMoving = false;

        public void PlayGame()
        {
            Time.timeScale = 1;
            playerPaused = false;
        }

        public void PauseGame()
        {
            Time.timeScale = 0;
            playerPaused = true;
        }

        public void LoadDebugScene()
        {
            MenuManager.Instance.HideAllViews();
            PlayerMustStopNow();
            SceneManager.LoadScene("Debug Scene");
        }

        public void BackFromDebugScene()
        {
            LoadCurrentLevel();
            MenuManager.Instance.StartGame();
        }
        
        public void LoadNextLevel()
        {
            _levelNumber++;
            LoadLevel(_levelNumber);
        }

        public void LoadCurrentLevel()
        {
            int levelToLoad = PlayerPrefs.GetInt("LevelSaved", 1);
            _levelNumber = levelToLoad;
            LoadLevel(_levelNumber);
        }
        
        public void GameOver()
        {
            AudioManager.Instance.PlaySounds(Constants.AUDIO_GAMEOVERSOUND);
            MenuManager.Instance.GameOverView();
        }
        
        public void LevelCompleted()
        {
            MenuManager.Instance.EndLevelView();
            if (_levelNumber == _totalLevels)
            {
                AudioManager.Instance.PlaySounds(Constants.AUDIO_GAMECOMPLETEDSOUND);
                MenuManager.Instance.GameCompletedView();
            }
            else
            {
                AudioManager.Instance.PlaySounds(Constants.AUDIO_ENDLEVELSOUND);
            }
        }
        
        public void AddDiamonds(int diamonds)
        {
            _totalDiamondCount += diamonds;
            _diamondCountAtEachLevel += diamonds;
            MenuManager.Instance.CallUpdateDiamondCount(_totalDiamondCount);
        }

        public void ShowDiamondCountAtLevelEnd() => MenuManager.Instance.CallShowDiamondCount(_diamondCountAtEachLevel);
        
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
            _diamondCountAtEachLevel = 0;
            if (levelID > _totalLevels || levelID == 0)
            {
                _levelNumber = 1;
                levelID = 1;
                PlayerPrefs.SetInt("LevelSaved", _levelNumber);
            }
            if (levelID != 0 && levelID <= _totalLevels)
            {
                AudioManager.Instance.PlaySounds(Constants.AUDIO_GAMESTARTSOUND);
                SceneManager.LoadScene("Level 0" + levelID);
                PlayerPrefs.SetInt("LevelSaved", _levelNumber);
                PlayerPrefs.Save();
            }
        }
    }
}
