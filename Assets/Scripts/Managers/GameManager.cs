using System;
using Environment_Setters;
using Player_Scripts;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        
        public static GameManager Instance;
        
        [Header(" GameObjects Imported")]
        [SerializeField] private PlayerController player;
        [SerializeField] private Path path;
        [SerializeField] private Level _level01;

        private Level _levelTBD;
        private Path _path;
        private PlayerController _player;
        
        private float _pathLength;
        private float _playerXValue;
        private float _playerYValue;
        private float _playerZValue;
        
        private int x = 1;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        

        void Start()
        {
            _pathLength = MetaData.Instance.scriptableInstance.pathLength;
            Init();
        }
      

        private void PlayerSetup()
        {
            //// Dynamic WayPoint/Path Implementation
            //_playerXValue = _path.transform.position.x - _pathLength/2 + player.transform.localScale.x;
            //_playerYValue = _path.transform.position.y + player.transform.localScale.y;
            //_playerZValue = _path.transform.position.z + player.transform.localScale.z;
            
            _playerXValue = LevelDecider().transform.position.x;
            _playerYValue = LevelDecider().transform.position.y + LevelDecider().transform.localScale.y/2 + player.transform.localScale.y/2 + 1.25f;
            _playerZValue = LevelDecider().transform.position.z - 7.5f + player.transform.localScale.z/2;
            Debug.Log(_playerYValue);
        }
        private void Init()
        {
            //// Dynamic WayPoint/Path Implementation
            //_path = Instantiate(path, new Vector3(0, 0, 0), Quaternion.identity);
            PlayerSetup();
            _player = Instantiate(player,new Vector3(_playerXValue, _playerYValue, _playerZValue), Quaternion.identity);
            Debug.Log(_player.transform.position);
            
        }

        private void LateUpdate()
        {
            if (x == 1)
            {
                _player.PlayerPositions(_level01.GiveWayPoints());
                x++;
            };
        }
        private Level LevelDecider()
        {
            _levelTBD = _level01;
            return _levelTBD;
        }
    }
    
}
