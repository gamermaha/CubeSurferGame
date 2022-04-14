using System;
using System.Net.Mime;
using Controllers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using DG.Tweening;
using Random = UnityEngine.Random;

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
        public GameObject diamondSprite;
        public Image  hUDDiamondImage;
        
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

        public void DiamondCountIncrement(int diamondCount)
        {
            diamondCountDisplay.text = "" + diamondCount;
        }

        public void SliderUpdate(float sliderValue)
        {
            mySlider.value = sliderValue;
        }

        public void DiamondAnimation(Vector3 instantiatePos, Camera cam)
        {
            GameObject diamond = Instantiate(diamondSprite);
            diamond.transform.SetParent(transform);
            diamond.transform.position = cam.WorldToScreenPoint(instantiatePos);
            
            // Vector3 targetPos = cam.ScreenToWorldPoint(hUDDiamondImagePos.transform.position);
            // Debug.Log(targetPos);
            diamond.transform.DOMove(hUDDiamondImage.transform.position, 0.35f)
                .SetEase(Ease.Unset).OnComplete(() =>
                {
                    Destroy(diamond);
                });

        }



    }
}
