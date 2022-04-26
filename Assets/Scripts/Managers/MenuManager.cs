using Controllers;
using UnityEngine;

namespace Managers
{
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager Instance;
        
        [SerializeField] private GameStartView _gameStartView;
        [SerializeField] private GameRestartView _gameRestartView;
        [SerializeField] private HUDView _hUDView;
        [SerializeField] private EndLevelView _endLevelView;
        [SerializeField] private GameOverView _gameOverView;
        [SerializeField] private GameCompletedView _gameCompletedView;
        
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
            _gameStartView.gameObject.SetActive(true);
            _gameRestartView.gameObject.SetActive(false);
            _hUDView.gameObject.SetActive(false);
            _endLevelView.gameObject.SetActive(false);
            _gameOverView.gameObject.SetActive(false);
            _gameCompletedView.gameObject.SetActive(false);
            StartCoroutine(_gameStartView.HandSlider());
        }

        public void StartGame()
        {
            StopCoroutine(_gameStartView.HandSlider());
            PlayGame();
        }
        public void PlayGame()
        {
            _gameStartView.gameObject.SetActive(false);
            _gameRestartView.gameObject.SetActive(false);
            _hUDView.gameObject.SetActive(true);
            GameManager.Instance.PlayerCanMoveNow();
        }
        public void RestartOnGameOverButton()
        {
            GameManager.Instance.PlayerMustStopNow();
            _gameOverView.gameObject.SetActive(false);
            _endLevelView.gameObject.SetActive(false);
            _gameRestartView.gameObject.SetActive(true);
            GameManager.Instance.LoadCurrentLevel();
        }
        
        public void EndLevel()
        {
            GameManager.Instance.LoadNextLevel();
            _endLevelView.gameObject.SetActive(false);
            _gameRestartView.gameObject.SetActive(true);
        }
        
        public void GameCompleted()
        {
            _gameCompletedView.gameObject.SetActive(false);
            _gameRestartView.gameObject.SetActive(true);
        }
        
        public void GameCompletedView()
        {
            _gameCompletedView.gameObject.SetActive(true);
            _endLevelView.gameObject.SetActive(false);
            _gameRestartView.gameObject.SetActive(false); 
        }

        public void EndLevelView() => _endLevelView.gameObject.SetActive(true);
        
        public void GameOverView() => _gameOverView.gameObject.SetActive(true);

        public void CallUpdateDiamondCount(int diamondCount) => _hUDView.UpdateDiamondCount(diamondCount);
        
        public void CallDiamondAnimation(Vector3 instantiatePos, Camera cam) => _hUDView.DiamondAnimation(instantiatePos, cam);
        
        public void CallDiamondAnimationTimesTwo(string display) => _hUDView.DiamondAnimationTimesTwo(display);

        public void CallSliderUpdate(float lengthCoveredPercentage)
        {
            _hUDView.SliderUpdate(lengthCoveredPercentage);
        }
    }
}
