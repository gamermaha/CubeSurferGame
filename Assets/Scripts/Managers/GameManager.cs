﻿using System;
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
        [SerializeField] private Level01 _level01;

        private float _pathLength;
       
        private float _playerXValue;
        private float _playerYValue;
        private float _playerZValue;
        private Path _path;
        private PlayerController _player;
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
            //_playerXValue = _path.transform.position.x - _pathLength/2 + player.transform.localScale.x;
            //_playerYValue = _path.transform.position.y + player.transform.localScale.y;
            //_playerZValue = _path.transform.position.z + player.transform.localScale.z;
            
            _playerXValue = _level01.transform.position.x - 7.5f + player.transform.localScale.x/2;
            _playerYValue = _level01.transform.position.y + _level01.transform.localScale.y + player.transform.localScale.y/2;
            _playerZValue = _level01.transform.position.z;
            
        }
        private void Init()
        {
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
    }
    
}
