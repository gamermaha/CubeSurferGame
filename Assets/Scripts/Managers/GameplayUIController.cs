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
        //public int levelNumber = 1;
        private int _totalLevels;
        public bool loadNextLevel;
        public bool loadSameLevel;
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
            loadSameLevel = true;
            GameManager.Instance.LoadNewLevel();

        }

        public void EndLevel()
        {
            if (GameManager.Instance.levelNumber < _totalLevels)
            {
                //levelNumber++;
                GameManager.Instance.levelNumber++;
                InputClass.startMoving = false;
                gameEndView.SetActive(false);
                gameStartView.SetActive(true);
                GameManager.Instance.LoadNewLevel();
                //loadNextLevel = true;
                //SceneManager.LoadScene("Level 0" + levelNumber);
            }
            else
            {
                GameCompleted();
            }
        }

        private void GameCompleted()
        {
            gameCompletedView.SetActive(true);
            gameEndView.SetActive(false);
            
        }




    }
}
