using System.Collections.Generic;
using Managers;
using UnityEngine;


namespace Player_Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        public bool wayPtFinished;
        
        [SerializeField] private GameObject player;
        private Vector3 _prevMousePos;
        private Vector3 _prevPlayerPos;

        private float _totalLength;
        private float _lengthCovered;
        private float _coveredDistanceInWayPoints;
        public float lengthCoveredPercentage;
        
        private List<Transform> _playerPositions;
        private Transform _startPos;
        
        private int _wayPtIncrement;
        private float _thresholdInWayPt;
        private float _halfPathWidth;
        private float _mySpeed;
        private double _cubeSize;
        private Animator _anim;
        public static bool startMoving = false;
        private Vector3 _rotation;
        private float _movementX;
        private float _movementY;
        private float _movementZ;
        private bool _onEnd;
        
        void Awake()
        {
            _anim = player.GetComponentInChildren<Animator>();
            _prevMousePos = new Vector3(0f, 0f, 0f);
            _thresholdInWayPt = 0.5f;
            _halfPathWidth = 3f;
            _coveredDistanceInWayPoints = 0;
        }
        private void Start()
        {
            if (MetaData.Instance == null)
            {
                _mySpeed = 0;
            }
            else
            {
                _mySpeed = MetaData.Instance.scriptableInstance.playerSpeed;
                _cubeSize = MetaData.Instance.scriptableInstance.cubeLength;
            }
            _onEnd = false;
            _lengthCovered = 0;
            lengthCoveredPercentage = 0;
            _coveredDistanceInWayPoints = 0;
        }

        void Update()
        {

            if (!startMoving)
                return;

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
                    if (distance <= _thresholdInWayPt )
                    {
                        if ( _wayPtIncrement >= 2 && _wayPtIncrement < (_playerPositions.Count - 1)) 
                        {
                            _coveredDistanceInWayPoints += Vector3.Distance(_playerPositions[_wayPtIncrement - 1].position,
                            _playerPositions[_wayPtIncrement-2].position);
                            
                        }
                        _wayPtIncrement++;
                    }
                    if (_wayPtIncrement >= _playerPositions.Count)
                    { 
                        
                        wayPtFinished = true;
                    }
                        
                }
                
                OnCenter();
                if (_wayPtIncrement == 0)
                    _lengthCovered = Vector3.Distance(transform.position, _startPos.position); 
            
                else if (_wayPtIncrement >= 1)
                {
                    _lengthCovered = Vector3.Distance(transform.position, _playerPositions[_wayPtIncrement - 1].position);
                    if (_wayPtIncrement >= 2)
                        _lengthCovered += _coveredDistanceInWayPoints;
                }
                
               
            }
            else if (wayPtFinished && _onEnd == false)
            {
                _lengthCovered = _totalLength;
                transform.Translate(0f, 0f, _mySpeed * Time.deltaTime);
            }
            
            // confirm jannati
           
            lengthCoveredPercentage =  _lengthCovered/_totalLength;
            GameplayUIController.Instance.SliderUpdate(lengthCoveredPercentage);
            // confirm jannati
            
            
            _prevPlayerPos = transform.position;


        }

        
        //// Helper Functions
        private void OnCenter()
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
        public void MoveUp(int up)
        {
            _anim.SetTrigger("jump");
            player.transform.Translate(0f, (float) _cubeSize * up, 0f);
        }

        public void MoveDown(int down)
        { 
            player.transform.Translate(0f, -1 * (float) _cubeSize * down, 0f);
        }
        private void MoveRight()
        {
            _prevMousePos = Input.mousePosition;
            transform.GetChild(0).localPosition = new Vector3(Mathf.Clamp(transform.GetChild(0).localPosition.x + 0.1f,-2.5f, 2.5f), 0f, 0f);
        }
        
        private void MoveLeft()
        {
            _prevMousePos = Input.mousePosition;
            transform.GetChild(0).localPosition = new Vector3(Mathf.Clamp(transform.GetChild(0).localPosition.x - 0.1f, -2.5f, 2.5f), 0f, 0f);
        }
        
        

        public void StopPlayer()
        {
            Debug.Log("Total length covered: " + _lengthCovered);
            _onEnd = true;
            transform.position += new Vector3(0f, 0f, 0f);
            startMoving = false;
        }

        public void SetStartPos(Transform startPos)
        {
            _startPos = startPos;
            _coveredDistanceInWayPoints = 0;
        }
        public void PlayerPositions(List<Transform> playerPositions)
        {
            
            _playerPositions = playerPositions;
            _totalLength = Vector3.Distance(_startPos.position,
                _playerPositions[0].position);

            for (int i = 0; i < _playerPositions.Count; i++)
            {
                if (i < _playerPositions.Count-1)
                    _totalLength += Vector3.Distance(_playerPositions[i].position,
                    _playerPositions[i + 1].position);
            }
            Debug.Log("Path length = " + _totalLength);
        }

        
    } 
}

