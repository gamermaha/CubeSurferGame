//REVIEW: remove unused usings
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
        public int levelNumber; //REVIEW: no need to make it public

        //REVIEW: no need of serialize them as all of these are initialized internaly
        [Header(" GameObjects Imported")]
        [SerializeField] private PlayerController player; 
        [SerializeField] private Path path;
        [SerializeField] private Level level01;
        [SerializeField] private GameplayUIController uIController;
        [SerializeField] private Slider slider;


        private PlayerMovement _inputManager; //REVIEW: rename this to _playerMovement
        private Level _levelTBD; //REVIEW: either remove level01 or this
        private Path _path; //REVIEW: remove as its not in use anymore
        private PlayerController _player; //REVIEW: rename _playerController;
        private int _diamondCount;
        
        private float _playerXValue; //REVIEW: remove
        private float _playerYValue; //REVIEW: remove
        private float _playerZValue; //REVIEW: remove

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
        //REVIEW: space
        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnLevelFinishLoading;
        }
        //REVIEW: space
        void Start()
        {
            //REVIEW: why you need to save its reference as its Singleton it self
            uIController = FindObjectOfType<GameplayUIController>();

            //REVIEW: it should load last saved level
            //REVIEW: use string interpolation or string.format for "Level 01"
            SceneManager.LoadScene("Level 01");

            //REVIEW: it should be loaded from prefs
            levelNumber = 1;
        }
        
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnLevelFinishLoading;
        }
        //REVIEW: space
        private void OnLevelFinishLoading(Scene scene, LoadSceneMode mode)
        {
            //REVIEW: use string interpolation or string.format for "Level 01"
            //REVIEW: use .Equals instead of ==
            //REVIEW: you can even skip this check 
            if (scene.name == "Level 0" + levelNumber)
            {
                Init();
            }
        }
        
        private void Init()
        {
            _player = Instantiate(player);
            _inputManager = _player.GetComponent<PlayerMovement>();
            _levelTBD = LevelDecider();
            _inputManager.SetStartPos(_levelTBD.StartPosition);
            //REVIEW: use the above method to update player position as well
            _player.transform.position = _levelTBD.StartPosition.position;
            _inputManager.PlayerPositions(_levelTBD.GiveWayPoints());


            //REVIEW: remove space
        }

        //REVIEW: rename this to GetCurrentLevel and add a null as well
        private Level LevelDecider()
        {
            return FindObjectOfType<Level>();
        }

        //REVIEW: rename this to GameOver
        public void GameOverCall()
        {
            AudioManager.Instance.PlaySounds(AudioManager.GAMEOVERSOUND);
            uIController.GameOver(); 
        }
        //REVIEW: space
        //REVIEW: LevelCompleted
        public void EndGameCall()
        {
            AudioManager.Instance.PlaySounds(AudioManager.GAMECOMPLETEDSOUND);
            uIController.EndGame();
        }
        //REVIEW: space
        //REVIEW: LoadLevel (int levelId)
        //REVIEW: also add a new function for LoadNextLevel () with no params as currentl level is already present in scope
        //REVIEW: 
        public void LoadNewLevel(string levelName)
        {
            SceneManager.LoadScene(levelName); 
            string activeScene = SceneManager.GetActiveScene().name;
            PlayerPrefs.SetString("LevelSaved", activeScene);
            Debug.Log(activeScene);
        }
        //REVIEW: space
        //REVIEW: rename AddDiamonds (int diamonds)
        public void DiamondCountUpdate(int addDiamonds)
        {
            _diamondCount += addDiamonds;
            //REVIEW: rename UpdateDiamondCount
            uIController.DiamondCountIncrement(_diamondCount);
        }
    }
}
