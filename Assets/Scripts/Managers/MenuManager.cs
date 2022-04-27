using Controllers;
using UnityEngine;

namespace Managers
{
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager Instance;
        
        [SerializeField] private GameStartView gameStartView;
        [SerializeField] private GameRestartView gameRestartView;
        [SerializeField] private HUDView hUDView;
        [SerializeField] private EndLevelView endLevelView;
        [SerializeField] private GameOverView gameOverView;
        [SerializeField] private GameCompletedView gameCompletedView;
        [SerializeField] private BaseView baseView;
        
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
            gameStartView.ShowView();
            
            
            gameRestartView.HideView();
            hUDView.ShowView();
            endLevelView.HideView();
            gameOverView.HideView();
            gameCompletedView.HideView();
            StartCoroutine(gameStartView.HandSlider());
        }

        public void StartGame()
        {
            StopCoroutine(gameStartView.HandSlider());
            PlayGame();
        }
        
        public void PlayGame()
        {
            gameStartView.HideView();
            gameRestartView.HideView();
            hUDView.ShowView();
            GameManager.Instance.PlayerCanMoveNow();
        }
        
        public void RestartOnGameOverButton()
        {
            GameManager.Instance.PlayerMustStopNow();
            gameOverView.HideView();
            endLevelView.HideView();
            gameRestartView.ShowView();
            GameManager.Instance.LoadCurrentLevel();
        }
        
        public void EndLevel()
        {
            GameManager.Instance.LoadNextLevel();
            endLevelView.HideView();
            gameRestartView.ShowView();
        }
        
        public void GameCompleted()
        {
            gameCompletedView.HideView();
            gameRestartView.ShowView();
        }
        
        public void GameCompletedView()
        {
            gameCompletedView.ShowView();
            endLevelView.HideView();
            gameRestartView.HideView(); 
        }

        public void HideAllViews()
        {
            gameStartView.HideView();
            gameRestartView.HideView();
            hUDView.HideView();
            endLevelView.HideView();
            gameOverView.HideView();
            gameCompletedView.HideView();
        }

        public void EndLevelView() => endLevelView.ShowView();
        
        public void GameOverView() => gameOverView.ShowView();

        public void CallUpdateDiamondCount(int diamondCount) => hUDView.UpdateDiamondCount(diamondCount);
        
        public void CallDiamondAnimation(Vector3 instantiatePos, Camera cam) => hUDView.DiamondAnimation(instantiatePos, cam);
        
        public void CallDiamondAnimationTimesTwo(string display) => hUDView.DiamondAnimationTimesTwo(display);

        public void CallSliderUpdate(float lengthCoveredPercentage) => hUDView.SliderUpdate(lengthCoveredPercentage);
    }
}
