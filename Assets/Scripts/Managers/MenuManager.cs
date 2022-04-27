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
            baseView.ShowView(gameStartView.gameObject);
            baseView.HideView(gameRestartView.gameObject);
            baseView.HideView(hUDView.gameObject);
            baseView.HideView(endLevelView.gameObject);
            baseView.HideView(gameOverView.gameObject);
            baseView.HideView(gameCompletedView.gameObject);
            StartCoroutine(gameStartView.HandSlider());
        }

        public void StartGame()
        {
            StopCoroutine(gameStartView.HandSlider());
            PlayGame();
        }
        
        public void PlayGame()
        {
            baseView.HideView(gameStartView.gameObject);
            baseView.HideView(gameRestartView.gameObject);
            baseView.ShowView(hUDView.gameObject);
            GameManager.Instance.PlayerCanMoveNow();
        }
        
        public void RestartOnGameOverButton()
        {
            GameManager.Instance.PlayerMustStopNow();
            baseView.HideView(gameOverView.gameObject);
            baseView.HideView(endLevelView.gameObject);
            baseView.ShowView(gameRestartView.gameObject);
            GameManager.Instance.LoadCurrentLevel();
        }
        
        public void EndLevel()
        {
            GameManager.Instance.LoadNextLevel();
            baseView.HideView(endLevelView.gameObject);
            baseView.ShowView(gameRestartView.gameObject);
        }
        
        public void GameCompleted()
        {
            baseView.HideView(gameCompletedView.gameObject);
            baseView.ShowView(gameRestartView.gameObject);
        }
        
        public void GameCompletedView()
        {
            baseView.ShowView(gameCompletedView.gameObject);
            baseView.HideView(endLevelView.gameObject);
            baseView.HideView(gameRestartView.gameObject); 
        }

        public void EndLevelView() => baseView.ShowView(endLevelView.gameObject);
        
        public void GameOverView() => baseView.ShowView(gameOverView.gameObject);

        public void CallUpdateDiamondCount(int diamondCount) => hUDView.UpdateDiamondCount(diamondCount);
        
        public void CallDiamondAnimation(Vector3 instantiatePos, Camera cam) => hUDView.DiamondAnimation(instantiatePos, cam);
        
        public void CallDiamondAnimationTimesTwo(string display) => hUDView.DiamondAnimationTimesTwo(display);

        public void CallSliderUpdate(float lengthCoveredPercentage) => hUDView.SliderUpdate(lengthCoveredPercentage);
    }
}
