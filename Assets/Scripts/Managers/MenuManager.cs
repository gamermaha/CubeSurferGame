using Controllers;
using Environment_Setters;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager Instance;
        
        [SerializeField] private GameStartView gameStartView;
        [SerializeField] private HUDView hUDView;
        [SerializeField] private EndLevelView endLevelView;
        [SerializeField] private GameOverView gameOverView;
        [SerializeField] private GameCompletedView gameCompletedView;
        [SerializeField] private CubeSelectionView cubeSelectionView;
        
        
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

        private void Start()
        {
            gameStartView.ShowView();
            hUDView.ShowView();
            endLevelView.HideView();
            gameOverView.HideView();
            gameCompletedView.HideView();
            cubeSelectionView.HideView();
            StartGame();
        }
        
        public void StartGame()
        {
            gameStartView.ShowView();
            hUDView.ShowView();
        }
        
        public void PlayGame()
        {
            gameStartView.HideView();
            hUDView.ShowView();
            GameManager.Instance.PlayerCanMoveNow();
        }
        
        public void RestartOnGameOverButton()
        {
            GameManager.Instance.PlayerMustStopNow();
            gameOverView.HideView();
            endLevelView.HideView();
            gameStartView.ShowView();
            GameManager.Instance.LoadCurrentLevel();
        }
        
        public void EndLevel()
        {
            GameManager.Instance.LoadNextLevel();
            endLevelView.HideView();
            gameStartView.ShowView();
            gameCompletedView.HideView();
        }
        
        
        public void GameCompletedView()
        {
            gameCompletedView.ShowView();
            gameStartView.HideView(); 
        }

        public void HideAllViews()
        {
            gameStartView.HideView();
            hUDView.HideView();
            endLevelView.HideView();
            gameOverView.HideView();
            gameCompletedView.HideView();
            cubeSelectionView.HideView();
        }

        public void EndLevelView()
        {
            endLevelView.ShowView();
            GameManager.Instance.ShowDiamondCountAtLevelEnd();
        }
        
        public void GameOverView() => gameOverView.ShowView();

        public void CallUpdateDiamondCount(int diamondCount)=> hUDView.UpdateDiamondCount(diamondCount);

        public void CallShowDiamondCount(int diamondCountAtEndLevel) => endLevelView.ShowDiamondCount(diamondCountAtEndLevel);

        public void CallDiamondAnimation(Vector3 instantiatePos, Camera cam) => hUDView.DiamondAnimation(instantiatePos, cam);
        
        public void CallDiamondAnimationTimesTwo(string display) => hUDView.DiamondAnimationTimesTwo(display);
        
        public void CallSliderUpdate(float lengthCoveredPercentage) => hUDView.SliderUpdate(lengthCoveredPercentage);
        public void CallShowConfetti(Vector3 confettiPos) => endLevelView.ShowConfetti(confettiPos); 

        public void ChangeCubeColourEnabled()
        {
            GameManager.Instance.PlayerMustStopNow();
            HideAllViews();
            cubeSelectionView.ShowView();
        }

        public void ChangeCubeColourDisabled()
        {
            cubeSelectionView.HideView();
            GameManager.Instance.BackFromDebugScene();
        }
    }
}
