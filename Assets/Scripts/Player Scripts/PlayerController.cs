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
                var trans = _playerPositions[_increment].transform.position;
                xValue = trans.x;
                yValue = trans.y;
                zValue = trans.z;
                
                if (Vector3.Distance(transform.position, _playerPositions[_increment].transform.position) <= 0.1) 
                    transform.position += new Vector3(xValue, yValue, zValue);
                else
                {
                    transform.position += new Vector3(_mySpeed, 0f, 0f)* _moveForce * Time.deltaTime;
                }
            } 
            
            
        }

        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Path"))
                _onPath = true;
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Path"))
            {
                Destroy(gameObject);
            }
                
        }

        public void PlayerPositions(List<GameObject> playerPositions)
        {
            _playerPositions = playerPositions;
            //Debug.Log(_playerPositions.Length);
            
        }
        
    }
}
