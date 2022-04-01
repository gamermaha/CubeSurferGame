using System;
using System.Collections.Generic;
using Managers;
using Player_Scripts;
using UnityEngine;

namespace Controllers
{
    public class InputClass : MonoBehaviour
    {
        [SerializeField] private Transform player;
        
        private Vector3 _prevMousePos;
        private Vector3 _prevPlayerPos;
        
        private List<Transform> _playerPositions;
        
        private int _wayPtIncrement;
        private float _thresholdInWayPt;
        private float _halfPathWidth;
        private float _mySpeed;
        private float _moveForce;
        private double _cubeSize;
        
        private bool _onEnd;
        
        
        void Awake()
        {
            Debug.Log("I am awake");
            _prevMousePos = new Vector3(0f, 0f, 0f);
            _thresholdInWayPt = 0.5f;
            _halfPathWidth = 3f;

        }
        private void Start()
        {
            if (MetaData.Instance == null)
            {
                _mySpeed = 0;
                _moveForce = 0;
            }
            else
            {
                _mySpeed = MetaData.Instance.scriptableInstance.playerSpeed;
                _moveForce = MetaData.Instance.scriptableInstance.playerForce;
            }
            _onEnd = false;
            _cubeSize = MetaData.Instance.scriptableInstance.cubeLength;
        }

        void Update()
        {
            if (_onEnd == false)
            {
                transform.position += new Vector3(0f, 0f, _mySpeed) * (_moveForce * Time.deltaTime);

                if ((transform.position.x <= _playerPositions[_wayPtIncrement].position.x + _halfPathWidth) &&
                    (transform.position.x >= _playerPositions[_wayPtIncrement].position.x - _halfPathWidth))
                    OnCenter();
                
                else if (transform.position.x > (_playerPositions[_wayPtIncrement].position.x + _halfPathWidth))
                    OnRightEdge();
                
                else if (transform.position.x < (_playerPositions[_wayPtIncrement].position.x - _halfPathWidth))
                    OnLeftEdge();
                
                _prevPlayerPos = transform.position;
                
            }
            else
            {
                StopPlayer();
            }
            if (_playerPositions.Count < _wayPtIncrement)
            {
                if (Vector3.Distance(transform.position, _playerPositions[_wayPtIncrement+1].position) <= _thresholdInWayPt) 
                    _wayPtIncrement++;
            }
            
            else
            {
                if (Vector3.Distance(transform.position, _playerPositions[_wayPtIncrement].position) <=
                    _thresholdInWayPt)
                {
                    StopPlayer();
                }
            }
            
        }

        
        //// Helper Functions
        public void OnCenter()
        {
            if (Input.GetMouseButton(0) && (Input.mousePosition.x - _prevMousePos.x) > 0)
            {
                MoveRight();
            }

            if (Input.GetMouseButton(0) && (Input.mousePosition.x - _prevMousePos.x) < 0)
            {
                MoveLeft();
            }
        }

        public void OnRightEdge()
        {
            if (Input.GetMouseButton(0) && (Input.mousePosition.x - _prevMousePos.x) < 0)
            {
                MoveLeft();
            }
        }

        public void OnLeftEdge()
        {
            if (Input.GetMouseButton(0) && (Input.mousePosition.x - _prevMousePos.x) > 0)
            {
                MoveRight();
            }
        }

        public void MoveUp(int up)
        {
            player.Translate(0f, (float) _cubeSize * up, 0f);
        }

        public void MoveDown(int down)
        {
            player.Translate(0f, -1 * (float) _cubeSize * down, 0f);
        }
        private void MoveRight()
        {
            Debug.Log("Moving to the right");
            _prevMousePos = Input.mousePosition;
            transform.Translate(0.1f, 0f, 0f);
        }
        
        private void MoveLeft()
        {
            Debug.Log("Moving to the left");
            _prevMousePos = Input.mousePosition;
            transform.Translate(-0.1f, 0f, 0f);
        }
        
        

        public void StopPlayer()
        {
            _onEnd = true;
            transform.position = _prevPlayerPos;
        }
        public void PlayerPositions(List<Transform> playerPositions) => _playerPositions = playerPositions;
    } 
}

