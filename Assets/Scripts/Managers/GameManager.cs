
using Controllers;
using Environment_Setters;
using Player_Scripts;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        
        public static GameManager Instance;
        public int levelNumber;
        
        [Header(" GameObjects Imported")]
        [SerializeField] private PlayerController player;
        [SerializeField] private Path path;
        [SerializeField] private Level level01;
        [SerializeField] private GameplayUIController uIController;
        [SerializeField] private Slider slider;


        private InputClass _inputManager;
        private Level _levelTBD;
        private Path _path;
        private PlayerController _player;
        private int _diamondCount;
        
        private float _playerXValue;
        private float _playerYValue;
        private float _playerZValue;
        
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
        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnLevelFinishLoading;
        }
        void Start()
        {
            
            uIController = FindObjectOfType<GameplayUIController>();
            SceneManager.LoadScene("Level 05");
            levelNumber = 5;
           
        }

        private void Update()
        {
            //slider.value = _inputManager.lengthCoveredPercentage;
            //Debug.Log(slider.value);
        }
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnLevelFinishLoading;
        }

        private void OnLevelFinishLoading(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "Level 0" + levelNumber)
            {
                Init();
            }
        }
        
        private void Init()
        {
            _player = Instantiate(player);
            _inputManager = _player.GetComponent<InputClass>();
            _levelTBD = LevelDecider();
            _inputManager.PlayerPositions(_levelTBD.GiveWayPoints());
            _player.transform.position = _levelTBD.StartPosition.position;
            _inputManager.SetStartPos(_levelTBD.StartPosition);
            
        }

        private Level LevelDecider()
        {
            return FindObjectOfType<Level>();
        }

        public void GameOverCall()
        {
            uIController.GameOver(); 
        }
        public void EndGameCall()
        {
            uIController.EndGame();
        }
        public void LoadNewLevel(string levelName)
        {
            SceneManager.LoadScene(levelName); 
            string activeScene = SceneManager.GetActiveScene().name;
            PlayerPrefs.SetString("LevelSaved", activeScene);
            Debug.Log(activeScene);
        }

        public void DiamondCountUpdate()
        {
            _diamondCount++;
            uIController.DiamondCountIncrement(_diamondCount);
        }
    }
    
}
