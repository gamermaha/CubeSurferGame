using System.Collections.Generic;
using Environment_Setters;
using Managers;
using UnityEngine;

namespace Player_Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        public bool wayPtFinished;
        public static bool StartMoving;
        public float lengthCoveredPercentage;
        
        [SerializeField] private GameObject playerCapsule;
        private Vector3 _prevMousePos;
        private Vector3 _prevTouchPos;

        private float _totalLength;
        private float _lengthCovered;
        private float _coveredDistanceInWayPoints;
        
        private List<Transform> _wayPoints;
        private int _wayPtIncrement;
        private Transform _startPlayerPos;
        private float _playerSpeed;
        private float _thresholdInWayPt = 0.5f;
        private float _halfPathWidth = 2.5f;
        private double _cubeSize;
        private Animator _playerAnimator;
        private bool _onEnd;
        
        void Awake()
        {
            _playerAnimator = playerCapsule.GetComponentInChildren<Animator>();
            _prevMousePos = new Vector3(0f, 0f, 0f);
            _coveredDistanceInWayPoints = 0;
        }
        private void Start()
        {
            if (MetaData.Instance == null)
            {
                _playerSpeed = 0;
            }
            else
            {
                _playerSpeed = MetaData.Instance.scriptableInstance.playerSpeed;
                _cubeSize = MetaData.Instance.scriptableInstance.cubeLength;
            }
            _onEnd = false;
            _lengthCovered = 0;
            lengthCoveredPercentage = 0;
            _coveredDistanceInWayPoints = 0;
        }

        void Update()
        {
            if (!StartMoving)
                return;

            if (transform == null || _wayPoints == null)
                return;
            
            if (!wayPtFinished)
            {  
                float distance = Vector3.Distance(_wayPoints[_wayPtIncrement].position, transform.position);
                transform.position = Vector3.MoveTowards(transform.position, _wayPoints[_wayPtIncrement].position, _playerSpeed * Time.deltaTime);

                var rotation = Quaternion.LookRotation(_wayPoints[_wayPtIncrement].position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);

                if (distance <= _thresholdInWayPt )
                {
                    if ( _wayPtIncrement >= 2 && _wayPtIncrement < (_wayPoints.Count - 1)) 
                    {
                        _coveredDistanceInWayPoints += Vector3.Distance(_wayPoints[_wayPtIncrement - 1].position,
                            _wayPoints[_wayPtIncrement-2].position);
                    }
                    _wayPtIncrement++;
                }
                if (_wayPtIncrement >= _wayPoints.Count)
                    wayPtFinished = true;

                if (_wayPtIncrement == 0)
                    _lengthCovered = Vector3.Distance(transform.position, _startPlayerPos.position); 
            
                else if (_wayPtIncrement >= 1)
                {
                    _lengthCovered = Vector3.Distance(transform.position, _wayPoints[_wayPtIncrement - 1].position);
                    if (_wayPtIncrement >= 2)
                        _lengthCovered += _coveredDistanceInWayPoints;
                }
            }
            else if (wayPtFinished && _onEnd == false)
            {
                _lengthCovered = _totalLength;
                transform.Translate(0f, 0f, _playerSpeed * Time.deltaTime);
            }
            lengthCoveredPercentage =  _lengthCovered/_totalLength;
            MenuManager.Instance.CallSliderUpdate(lengthCoveredPercentage);
        }

        private void LateUpdate()
        {
            if (!StartMoving)
                return;
            MovePlayerRightOrLeft();
        }

        public void SetStartPos(Transform startPos) => _startPlayerPos = startPos;
        
        public void PlayerPositions(List<Transform> playerPositions)
        {
            _wayPoints = playerPositions;
            _totalLength = Vector3.Distance(_startPlayerPos.position,
                _wayPoints[0].position);
            
            int i = 0;
            while (i < _wayPoints.Count-1)
            {
                _totalLength += Vector3.Distance(_wayPoints[i].position,
                    _wayPoints[i + 1].position);
                i++;
            }
        }
        
        public void StopPlayer()
        {
            _onEnd = true;
            transform.position += new Vector3(0f, 0f, 0f);
            StartMoving = false;
        }
        
        public void MoveUp(int up)
        {
            _playerAnimator.SetTrigger(Constants.PLAYER_ANIMATION_STATE);
            playerCapsule.transform.Translate(0f, (float) _cubeSize * up, 0f);
        }

        public void MoveDown(int down) => playerCapsule.transform.Translate(0f, -1 * (float) _cubeSize * down, 0f);
        
        private void MovePlayerRightOrLeft()
        {
            #if UNITY_EDITOR
                if (Input.GetMouseButton(0) && (Input.mousePosition.x - _prevMousePos.x) > 0)
                    MoveRightMouseIP();
                
                if (Input.GetMouseButton(0) && (Input.mousePosition.x - _prevMousePos.x) < 0)
                    MoveLeftMouseIP();
                
            #elif UNITY_ANDROID
                if (Input.touches.Length > 0)
                { 
                    Touch touch = Input.touches[0];

                    if (touch.phase == TouchPhase.Moved)
                    {
                        if (Input.touches[0].position.x - _prevTouchPos.x > 0)
                            MoveRightTouchIP();
                        if (Input.touches[0].position.x - _prevTouchPos.x < 0)
                            MoveLeftTouchIP();
                    }
                }
            
            #endif
        }
        
        private void MoveRightTouchIP()
        {
            _prevTouchPos = Input.touches[0].position;
            transform.GetChild(0).localPosition = new Vector3(Mathf.Clamp(transform.GetChild(0).localPosition.x + 0.1f,-_halfPathWidth, _halfPathWidth), 0f, 0f);
        }
        
        private void MoveLeftTouchIP()
        {
            _prevTouchPos = Input.touches[0].position;
            transform.GetChild(0).localPosition = new Vector3(Mathf.Clamp(transform.GetChild(0).localPosition.x - 0.1f, -_halfPathWidth, _halfPathWidth), 0f, 0f);
        }
        
        private void MoveRightMouseIP()
        {
            _prevMousePos = Input.mousePosition;
            transform.GetChild(0).localPosition = new Vector3(Mathf.Clamp(transform.GetChild(0).localPosition.x + 0.1f,-_halfPathWidth, _halfPathWidth), 0f, 0f);
        }
        
        private void MoveLeftMouseIP()
        {
            _prevMousePos = Input.mousePosition;
            transform.GetChild(0).localPosition = new Vector3(Mathf.Clamp(transform.GetChild(0).localPosition.x - 0.1f, -_halfPathWidth, _halfPathWidth), 0f, 0f);
        }
    } 
}

