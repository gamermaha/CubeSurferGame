
using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Player_Scripts
{
    public class PlayerController : MonoBehaviour
    {
        private float _mySpeed;
        private float _moveForce;
        
        private Rigidbody2D _myBody;
        
        private int _increment;
        
        private float _xValue;
        private float _yValue;
        private float _zValue;
        
        private Vector3 _prevMousePos;
        private Vector3 _prevPlayerPos;

        private List<GameObject> _playerPositions;

        void Awake()
        {
            _myBody = GetComponent<Rigidbody2D>();
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
        }
        private void Update()
        {
            if (PlayerCollider.AddCube)
            {
                transform.Translate(0f, 1f, 0f);
            }
            PlayerCollider.AddCube = false;
            
            if (PlayerCollider.DestroyCube)
            {
                transform.Translate(0f, -1f, 0f);
            }
            PlayerCollider.DestroyCube = false;
            
            if (Cubes.OnPath && _playerPositions != null)
            {
                transform.position += new Vector3(0f, 0f, _mySpeed) * (_moveForce * Time.deltaTime);

                
                if (transform.position.x <= _playerPositions[_increment].transform.position.x + 2f &&
                    transform.position.x >= _playerPositions[_increment].transform.position.x - 2f)
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
                else if (transform.position.x > _playerPositions[_increment].transform.position.x + 2f)
                {
                    if (Input.GetMouseButton(0) && (Input.mousePosition.x - _prevMousePos.x) < 0)
                    {
                        MoveLeft();
                    }
                }
                else if (transform.position.x < _playerPositions[_increment].transform.position.x - 2f)
                {
                    if (Input.GetMouseButton(0) && (Input.mousePosition.x - _prevMousePos.x) > 0)
                    {
                        MoveRight();
                    }
                }
                
                _prevPlayerPos = transform.position;
                if (Vector3.Distance(transform.position, _playerPositions[_increment+1].transform.position) <= 2 && _playerPositions.Count < _increment) 
                    _increment++;
            }
            else if (Cubes.OnPath == false)
                transform.position = _prevPlayerPos;
            
        }
        
        public void PlayerPositions(List<GameObject> playerPositions) => _playerPositions = playerPositions;
        
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
        
    }
}
