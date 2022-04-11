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
        private Vector3 _rotation;
        private float _movementX;
        private float _movementY;
        private float _movementZ;

        private string upDown = "Up"; 
        private bool _onEnd;
        public bool wayPtFinished;

        private InputClass Instance;
        
        void Awake()
        {
            _anim = player.GetComponentInChildren<Animator>();
            //Debug.Log(_anim.name);
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
                _cubeSize = MetaData.Instance.scriptableInstance.cubeLength;
            }
            _onEnd = false;
            _lengthCovered = 0;
            lengthCoveredPercentage = 0;
        }

        void Update()
        {
            // Debug.Log(_onEnd);
            if (!startMoving)
            {
                //Debug.Log("Value of start moving is "+startMoving);
                return;
            }

            if (transform == null || _playerPositions == null)
                return;
            

            if (!wayPtFinished)
            {  
                float distance = Vector3.Distance(_playerPositions[_wayPtIncrement].position, transform.position);
                transform.position =
                    Vector3.MoveTowards(transform.position, _playerPositions[_wayPtIncrement].position, _mySpeed * Time.deltaTime);

                var rotation = Quaternion.LookRotation(_playerPositions[_wayPtIncrement].position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);

                
                if (_playerPositions != null)
                {
                    if (distance <= _thresholdInWayPt)
                        _wayPtIncrement++;

                    if (_wayPtIncrement >= _playerPositions.Count)
                    {
                        //StopPlayer();
                        //transform.position += new Vector3(0f, 0f, 1f);
                        wayPtFinished = true;
                    }
                        
                }
                
                OnCenter();
                
                // if ((this.transform.GetChild(0).localPosition.x <= _playerPositions[_wayPtIncrement].position.x + _halfPathWidth - _cubeSize/2) &&
                //     (this.transform.position.x >= _playerPositions[_wayPtIncrement].position.x - _halfPathWidth + _cubeSize/2))
                //     OnCenter();
                //
                // else if (this.transform.GetChild(0).localPosition.x > (_playerPositions[_wayPtIncrement].position.x + _halfPathWidth - _cubeSize/2))
                //     OnRightEdge();
                //
                // else if (this.transform.GetChild(0).localPosition.x < (_playerPositions[_wayPtIncrement].position.x - _halfPathWidth + _cubeSize/2))
                //     OnLeftEdge();
                
                //_prevPlayerPos = transform.position;
                
                
                // confirm jannati
                if (Vector3.Distance(transform.position, _playerPositions[_playerPositions.Count-1].position) > 0)
                    _lengthCovered = Vector3.Distance(transform.position, _playerPositions[_playerPositions.Count-1].position);
                else
                {
                    _lengthCovered = _totalLength;
                }
                lengthCoveredPercentage =  _lengthCovered/_totalLength;
                // confirm jannati
            }
            else if (wayPtFinished && _onEnd == false)
            {
                transform.Translate(0f, 0f, 0.025f);
                Debug.Log("No more way points");
            }

            // if (_playerPositions != null)
            // {
            //     
            //     if (_wayPtIncrement < _playerPositions.Count)
            //     {
            //         if (Vector3.Distance(transform.position, _playerPositions[_wayPtIncrement + 1].position) <=
            //             _thresholdInWayPt)
            //         {
            //             _wayPtIncrement++;
            //         }
            //     }
            //
            //     else
            //     {
            //         if (Vector3.Distance(transform.position, _playerPositions[_wayPtIncrement].position) <=
            //             _thresholdInWayPt)
            //         {
            //             StopPlayer();
            //         }
            //     }
            // }
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
            Debug.Log("I am in down function.");
            //_anim.SetTrigger("jump");
            player.transform.Translate(0f, -1 * (float) _cubeSize * down, 0f);
        }
        private void MoveRight()
        {
            // Debug.Log("Moving to the right");
            _prevMousePos = Input.mousePosition;
            //transform.GetChild(0).Translate(0.1f, 0f, 0f);
            var childLocalPos = transform.GetChild(0).localPosition;
            transform.GetChild(0).localPosition = new Vector3(Mathf.Clamp(transform.GetChild(0).localPosition.x + 0.1f,-3, 3), 0f, 0f);
        }
        
        private void MoveLeft()
        {
            // Debug.Log("Moving to the left");
            _prevMousePos = Input.mousePosition;
            //transform.GetChild(0).Translate(-0.1f, 0f, 0f);
            transform.GetChild(0).localPosition = new Vector3(Mathf.Clamp(transform.GetChild(0).localPosition.x - 0.1f, -3, 3), 0f, 0f);
        }
        
        

        public void StopPlayer()
        {
            _onEnd = true;
            //gameEndButton.SetActive(true);
            transform.position += new Vector3(0f, 0f, 0f);
            startMoving = false;
            //GameplayUIController.EndGame();
        }


        public void PlayerPositions(List<Transform> playerPositions)
        {
            
            _playerPositions = playerPositions;
            _totalLength = Vector3.Distance(_playerPositions[0].position,
                _playerPositions[_playerPositions.Count - 1].position);
            //Debug.Log("Total Length "+_totalLength);
        }
    } 
}

