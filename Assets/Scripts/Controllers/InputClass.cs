using System.Collections.Generic;
using Managers;
using UnityEngine;


namespace Controllers
{
    public class InputClass : MonoBehaviour
    {
        public bool wayPtFinished;
        
        [SerializeField] private GameObject player;
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
            }
            else
            {
                _mySpeed = MetaData.Instance.scriptableInstance.playerSpeed;
                _cubeSize = MetaData.Instance.scriptableInstance.cubeLength;
            }
            _onEnd = false;
            _lengthCovered = 0;
            lengthCoveredPercentage = 0;
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
                    if (distance <= _thresholdInWayPt)
                        _wayPtIncrement++;

                    if (_wayPtIncrement >= _playerPositions.Count)
                    { 
                        wayPtFinished = true;
                    }
                        
                }
                
                OnCenter();
                
                // confirm jannati
                _lengthCovered = Vector3.Distance(transform.position, _playerPositions[0].position);
                lengthCoveredPercentage =  _lengthCovered/_totalLength;
                //Debug.Log(lengthCoveredPercentage);
                GameplayUIController.Instance.SliderUpdate(lengthCoveredPercentage);
                // confirm jannati
            }
            else if (wayPtFinished && _onEnd == false)
            {
                transform.Translate(0f, 0f, _mySpeed * Time.deltaTime);
                Debug.Log("No more way points");
            }

           
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
            transform.GetChild(0).localPosition = new Vector3(Mathf.Clamp(transform.GetChild(0).localPosition.x + 0.1f,-3, 3), 0f, 0f);
        }
        
        private void MoveLeft()
        {
            _prevMousePos = Input.mousePosition;
            transform.GetChild(0).localPosition = new Vector3(Mathf.Clamp(transform.GetChild(0).localPosition.x - 0.1f, -3, 3), 0f, 0f);
        }
        
        

        public void StopPlayer()
        {
            _onEnd = true;
            transform.position += new Vector3(0f, 0f, 0f);
            startMoving = false;
        }


        public void PlayerPositions(List<Transform> playerPositions)
        {
            
            _playerPositions = playerPositions;
            for (int i = 0; i < _playerPositions.Count; i++)
            {
                if (i < _playerPositions.Count-1)
                    _totalLength += Vector3.Distance(_playerPositions[i].position,
                    _playerPositions[i + 1].position);
            }
            Debug.Log(_totalLength);
        }
    } 
}

