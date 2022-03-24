using System;
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
        
        private bool _onPath = true;
        private int _increment = 0;
        
        private float xValue;
        private float yValue;
        private float zValue;
        
        private Vector3 prevMousePos;
        private Vector3 prevPlayerPos;

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
            if (_onPath && _playerPositions != null)
            {
                transform.position += new Vector3(_mySpeed, 0f, 0f) * (_moveForce * Time.deltaTime);
                

                if (transform.position.z <= _playerPositions[_increment].transform.position.z + 1f &&
                        transform.position.z >= _playerPositions[_increment].transform.position.z - 1f)
                {
                        if (Input.GetMouseButton(0) && (Input.mousePosition.x - prevMousePos.x) > 0)
                        {
                            MoveRight();
                        }

                        if (Input.GetMouseButton(0) && (Input.mousePosition.x - prevMousePos.x) < 0)
                        {
                            MoveLeft();
                        }
                }
                else if (transform.position.z > _playerPositions[_increment].transform.position.z + 1f)
                {
                    if (Input.GetMouseButton(0) && (Input.mousePosition.x - prevMousePos.x) < 0)
                    {
                        MoveLeft();
                    }
                }
                else if (transform.position.z < _playerPositions[_increment].transform.position.z - 1f)
                {
                    if (Input.GetMouseButton(0) && (Input.mousePosition.x - prevMousePos.x) > 0)
                    {
                        MoveRight();
                    }
                }
                prevPlayerPos = transform.position;
                if (Vector3.Distance(transform.position, _playerPositions[_increment+1].transform.position) <= 2 && _playerPositions.Count < _increment) 
                    _increment++;
            }
        }

        
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Path"))
                _onPath = true;
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.gameObject.CompareTag("Path"))
            {
                _onPath = false;
                Debug.Log(_onPath);
                transform.position = prevPlayerPos;
            }
                
        }

        public void PlayerPositions(List<GameObject> playerPositions) => _playerPositions = playerPositions;
        
        private void MoveRight()
        {
            Debug.Log("Moving to the right");
            prevMousePos = Input.mousePosition;
            transform.Translate(0f, 0f, 0.1f);
        }
        
        private void MoveLeft()
        {
            Debug.Log("Moving to the left");
            prevMousePos = Input.mousePosition;
            transform.Translate(0f, 0f, -0.1f);
        }
        
    }
}
