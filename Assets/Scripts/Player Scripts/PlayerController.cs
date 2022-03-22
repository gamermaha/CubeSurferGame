using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Player_Scripts
{
    public class PlayerController : MonoBehaviour
    {
        private float _mySpeed;
        private Rigidbody2D _myBody;
        private float _moveForce;
        private bool _onPath = true;
        private int _increment = 0;
        float xValue;
        float yValue;
        float zValue;
        private int x = 1;
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
            if (_onPath)
            {
                transform.position += new Vector3(_mySpeed, 0f, 0f) * (_moveForce * Time.deltaTime);

                if (transform.position.z <= 1f && transform.position.z >= -1f)
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
                else if (transform.position.z > 1f)
                {
                    if (Input.GetMouseButton(0) && (Input.mousePosition.x - prevMousePos.x) < 0)
                    {
                        MoveLeft();
                    }
                }
                else if (transform.position.z < -1f)
                {
                    if (Input.GetMouseButton(0) && (Input.mousePosition.x - prevMousePos.x) > 0)
                    {
                        MoveRight();
                    }
                }

                if (_playerPositions != null)
                {

                    if (Vector3.Distance(transform.position, _playerPositions[_increment].transform.position) <= 2 &&
                        _playerPositions.Count < _increment)
                    {
                        Debug.Log("I am here");
                        var trans = _playerPositions[_increment].transform.position;
                        xValue = trans.x;
                        yValue = trans.y;
                        zValue = trans.z;
                        transform.position = new Vector3(xValue, yValue, zValue);
                        _increment++;
                    }
                    else
                    {
                        xValue = _mySpeed * _moveForce * Time.deltaTime;
                        yValue = 0f;
                        zValue = 0f;
                        transform.position += new Vector3(xValue, yValue, zValue);
                    }

                    Debug.Log(transform.position);
                }

                prevPlayerPos = transform.position;

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
                //Destroy(gameObject);
                transform.position = prevPlayerPos;
            }
                
        }

        public void PlayerPositions(List<GameObject> playerPositions)
        {
            //_playerPositions = playerPositions;
            
        }
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
