using System;

using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Player_Scripts;

namespace Managers
{
    public class GameplayUIController : MonoBehaviour
    {
        public static GameplayUIController Instance;
        public GameObject gameStartView;
        public GameObject gameRestartView;
        public GameObject diamondSprite;
        public GameObject hUDView;
        public GameObject gameEndView;
        public GameObject gameOverView;
        public GameObject gameCompletedView;
        public Text diamondCountDisplay;
        public Text times2;
        public Image hUDDiamondImage;
        public Slider mySlider;
        
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
        private void Start() => mySlider.value = 0;
        
        public void NewGame()
        {
            GameManager.Instance.LoadFirstLevel();
            DisableSlider();
            
        }
        public void LoadGame()
        {
            GameManager.Instance.LoadCurrentLevel();
        }
        public void DisableSlider()
        {
            gameStartView.SetActive(false);
            gameRestartView.SetActive(false);
            hUDView.SetActive(true);
            PlayerMovement.startMoving = true;
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
            PlayerMovement.startMoving = false;
            gameOverView.SetActive(false);
            gameEndView.SetActive(false);
            gameRestartView.SetActive(true);
            GameManager.Instance.LoadCurrentLevel();

        }

        public void EndLevel()
        {
            GameManager.Instance.LoadNextLevel();
            gameEndView.SetActive(false);
            gameRestartView.SetActive(true);
        }
        
        

        public void GameCompleted()
        {
            
            gameCompletedView.SetActive(true);
            gameEndView.SetActive(false);
            
        }

        public void UpdateDiamondCount(int diamondCount) => diamondCountDisplay.text = "" + diamondCount;

        public void SliderUpdate(float sliderValue) => mySlider.value = sliderValue;

        public void DiamondAnimation(Vector3 instantiatePos, Camera cam)
        {
            GameObject diamond = Instantiate(diamondSprite);
            diamond.transform.SetParent(transform);
            diamond.transform.position = cam.WorldToScreenPoint(instantiatePos);
            diamond.transform.DOMove(hUDDiamondImage.transform.position, 0.35f)
                .SetEase(Ease.Unset).OnComplete(() =>
                {
                    Destroy(diamond);
                });

        }

        public void DiamondAnimationTimesTwo(string display)
        {
            times2.transform.position = hUDDiamondImage.transform.position;
            times2.text = display;
        }
    }
}
