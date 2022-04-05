using System;
using System.Collections.Generic;
using Managers;
using Player_Scripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Controllers
{
    public class InputClass : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        //[SerializeField] private GameObject gameEndButton;
        //[SerializeField] private Animator anim;
        private Vector3 _prevMousePos;
        private Vector3 _prevPlayerPos;

        private float _totalLength;
        private float _lengthCovered;
        public float lengthCoveredPercentage;
        
        private List<Transform> _playerPositions;
        
        private int _wayPtIncrement;
        private float _thresholdInWayPt;
        private float _halfPathWidth;
        private float _mySpeed;
        private float _moveForce;
        private double _cubeSize;
        private Animator _anim;
        public static bool startMoving = false;

        private string upDown = "Up"; 
        private bool _onEnd;
        
        
        void Awake()
        {
            _anim = player.GetComponentInChildren<Animator>();
            Debug.Log(_anim.name);
            _prevMousePos = new Vector3(0f, 0f, 0f);
            _thresholdInWayPt = 0.05f;
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
                _cubeSize = MetaData.Instance.scriptableInstance.cubeLength;
            }
            _onEnd = false;
        }

        void Update()
        {
            // Debug.Log(_onEnd);
            if (!startMoving)
            {
                Debug.Log("Value of start moving is "+startMoving);
                return;
            }

            if (!_onEnd)
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

                if (Vector3.Distance(new Vector3(0f, 0f, transform.position.z), new Vector3(0f, 0f, _playerPositions[_playerPositions.Count-1].position.z)) > 0)
                    _lengthCovered = Vector3.Distance(new Vector3(0f, 0f, transform.position.z), new Vector3(0f, 0f, _playerPositions[_playerPositions.Count-1].position.z));
                else
                {
                    _lengthCovered = _totalLength;
                }
                lengthCoveredPercentage =  _lengthCovered/_totalLength;

                

            }
            else
            {
                StopPlayer();
            }

            if (_playerPositions != null)
            {
                
                if (_playerPositions.Count < _wayPtIncrement)
                {
                    if (Vector3.Distance(transform.position, _playerPositions[_wayPtIncrement + 1].position) <=
                        _thresholdInWayPt)
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
            _anim.SetTrigger("jump");
            player.transform.Translate(0f, (float) _cubeSize * up, 0f);
        }

        public void MoveDown(int down)
        {
            //_anim.SetTrigger("jump");
            player.transform.Translate(0f, -1 * (float) _cubeSize * down, 0f);
        }
        private void MoveRight()
        {
            // Debug.Log("Moving to the right");
            _prevMousePos = Input.mousePosition;
            transform.Translate(0.1f, 0f, 0f);
        }
        
        private void MoveLeft()
        {
            // Debug.Log("Moving to the left");
            _prevMousePos = Input.mousePosition;
            transform.Translate(-0.1f, 0f, 0f);
        }
        
        

        public void StopPlayer()
        {
            _onEnd = true;
            //gameEndButton.SetActive(true);
            transform.position = _prevPlayerPos;
            //GameplayUIController.EndGame();
        }


        public void PlayerPositions(List<Transform> playerPositions)
        {
            _playerPositions = playerPositions;
            _totalLength = Vector3.Distance(_playerPositions[0].position,
                _playerPositions[_playerPositions.Count - 1].position);
            Debug.Log("Total Length "+_totalLength);
        }
    } 
}

