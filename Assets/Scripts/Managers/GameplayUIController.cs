using System;
using System.Net.Mime;
using Controllers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Managers
{
    public class GameplayUIController : MonoBehaviour
    {
        public static GameplayUIController Instance;
        //public float sliderValue;
        public GameObject gameStartView;
        public Slider mySlider;
        public GameObject gameRestartView;
        public GameObject hUDView;
        public GameObject gameEndView;
        public GameObject gameOverView;
        public GameObject gameCompletedView;
        public Text diamondCountDisplay; 
        private int _diamondCount;
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
        private void Start()
        {
            _totalLevels = MetaData.Instance.scriptableInstance.noOflevels;
            mySlider.value = 0;
        }

        public void NewGame()
        {
            GameManager.Instance.levelNumber = 1;
            GameManager.Instance.LoadNewLevel("Level 01");
            DisableSlider();
            
        }
        public void LoadGame()
        {
            if (PlayerPrefs.HasKey("LevelSaved"))
            {
                //GameManager.Instance.levelNumber = 1;
                string levelToLoad = PlayerPrefs.GetString("LevelSaved");
                string levelno = levelToLoad.Substring(levelToLoad.Length - 1);
                
                if (levelToLoad != "Level 05" || levelToLoad != "SplashScreen")
                {
                    GameManager.Instance.LoadNewLevel(levelToLoad);
                    GameManager.Instance.levelNumber = Int32.Parse(levelno);
                }
            }
        }
        public void DisableSlider()
        {
            gameStartView.SetActive(false);
            gameRestartView.SetActive(false);
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
            gameRestartView.SetActive(true);
            GameManager.Instance.LoadNewLevel("Level 0" + GameManager.Instance.levelNumber);

        }

        public void EndLevel()
        {
            if (GameManager.Instance.levelNumber < _totalLevels)
            {
                GameManager.Instance.levelNumber++;
                InputClass.startMoving = false;
                gameEndView.SetActive(false);
                gameRestartView.SetActive(true);
                GameManager.Instance.LoadNewLevel("Level 0" + GameManager.Instance.levelNumber);
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

        public void DiamondCountIncrement()
        {
            Debug.Log("I am in diamond display");
            _diamondCount++;
            diamondCountDisplay.text = "" + _diamondCount;
        }

        public void SliderUpdate(float sliderValue)
        {
            mySlider.value = sliderValue;
        }



    }
}
