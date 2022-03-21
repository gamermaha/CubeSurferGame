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
        private void FixedUpdate()
        {
            if (_playerPositions != null)
            {
                
                //Debug.Log(trans);

                if (Vector3.Distance(transform.position, _playerPositions[_increment].transform.position) <= 2 && _playerPositions.Count < _increment)
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
                Destroy(gameObject);
            }
                
        }

        public void PlayerPositions(List<GameObject> playerPositions)
        {
            _playerPositions = playerPositions;
            //Debug.Log(_playerPositions[1].transform.position);
        }
        
    }
}
