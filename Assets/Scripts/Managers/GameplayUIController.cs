using System;
using Controllers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Managers
{
    public class GameplayUIController : MonoBehaviour
    {
        public GameObject gameStartView;
        public GameObject hUDView;
        public GameObject gameEndView;
        public GameObject gameOverView;
        public GameObject gameCompletedView;

        private int _totalLevels;
        private GameplayUIController Instance;

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
        private void Start()
        {
            _totalLevels = MetaData.Instance.scriptableInstance.noOflevels;
        }

        public void DisableSlider()
        {
            gameStartView.SetActive(false);
            hUDView.SetActive(true);
            InputClass.startMoving = true;
        }
        public void EndGame()
        {
            gameEndView.SetActive(true);
        }
        public void GameOver()
        {
            gameOverView.SetActive(true);
        }

        public void RestartGame()
        {
            InputClass.startMoving = false;
            gameOverView.SetActive(false);
            gameEndView.SetActive(false);
            gameStartView.SetActive(true);
            GameManager.Instance.LoadNewLevel("Level 0" + GameManager.Instance.levelNumber);

        }

        public void EndLevel()
        {
            if (GameManager.Instance.levelNumber < _totalLevels)
            {
                GameManager.Instance.levelNumber++;
                InputClass.startMoving = false;
                gameEndView.SetActive(false);
                gameStartView.SetActive(true);
                GameManager.Instance.LoadNewLevel("Level 0" + GameManager.Instance.levelNumber);
            }
            else
            {
                GameCompleted();
            }
        }
        public void LoadGame()
        {
            if (PlayerPrefs.HasKey("LevelSaved"))
            {
                string levelToLoad = PlayerPrefs.GetString("LevelSaved");
                GameManager.Instance.LoadNewLevel(levelToLoad);
            }
        }

        private void GameCompleted()
        {
            gameCompletedView.SetActive(true);
            gameEndView.SetActive(false);
            
        }




    }
}
