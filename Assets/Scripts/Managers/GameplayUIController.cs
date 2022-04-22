using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Managers
{
    public class GameplayUIController : MonoBehaviour
    {
        public static GameplayUIController Instance;
        public GameObject gameStartView;
        public GameObject gameRestartView;
        public GameObject hUDView;
        public GameObject endLevelView;
        public GameObject gameOverView;
        public GameObject gameCompletedView;
        public GameObject diamondSprite;
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

        private void Start()
        {
            mySlider.value = 0;
            gameRestartView.SetActive(false);
        }

        public void PlayGameButton()
        {
            GameManager.Instance.LoadCurrentLevel();
            DisableSlider();
        }

        public void DisableSlider()
        {
            gameStartView.SetActive(false);
            gameRestartView.SetActive(false);
            hUDView.SetActive(true);
            GameManager.Instance.PlayerCanMoveNow();
        }
        
        public void RestartOnGameOverButton()
        {
            GameManager.Instance.PlayerMustStopNow();
            gameOverView.SetActive(false);
            endLevelView.SetActive(false);
            gameRestartView.SetActive(true);
            GameManager.Instance.LoadCurrentLevel();
        }
        
        public void EndLevelButton()
        {
            GameManager.Instance.LoadNextLevel();
            endLevelView.SetActive(false);
            gameRestartView.SetActive(true);
        }
        
        public void GameCompletedView()
        {
            gameCompletedView.SetActive(true);
            endLevelView.SetActive(false);
            gameRestartView.SetActive(false);
        }
        
        public void EndLevelView() => endLevelView.SetActive(true);
        
        public void GameOverView() => gameOverView.SetActive(true);

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
