using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player_Scripts
{
    public class SamplePlayerScript : MonoBehaviour
    {
        
        [SerializeField] private List<Transform> wayPoints;
        
        private float _mySpeed = 0.1f;
        private float _moveForce = 10f;
        private int _x = 1;
        private int _wayPtIncrement = 0;
        private float _thresholdInWayPt = 5f;
        private Vector3 _rotation;
        private float _movementX;
        private float _movementY;
        private float _movementZ;


        void Update()
        {
            StartCoroutine(PlayerMovement());
        }

        IEnumerator PlayerMovement()
        {
             
            while (_wayPtIncrement + 1 < wayPoints.Count)
            {
                if (Math.Abs(wayPoints[_wayPtIncrement].position.x - wayPoints[_wayPtIncrement + 1].position.x) != 0)
                {
                    Debug.Log("I am in x change");
                    _movementX = wayPoints[_wayPtIncrement + 1].position.x;
                    _movementY = transform.position.y;
                    _movementZ = transform.position.z;
                }
                else if (Math.Abs(wayPoints[_wayPtIncrement].position.z - wayPoints[_wayPtIncrement + 1].position.z) != 0)
                {
                    Debug.Log("I am in z change");
                    _movementX = transform.position.x;
                    _movementY = transform.position.y;
                    _movementZ = wayPoints[_wayPtIncrement + 1].position.z;
                }
                else if (Math.Abs(wayPoints[_wayPtIncrement].position.x - wayPoints[_wayPtIncrement + 1].position.x) !=
                         0 && Math.Abs(
                             wayPoints[_wayPtIncrement].position.z - wayPoints[_wayPtIncrement + 1].position.z) != 0)
                {
                    Debug.Log("I am in both change");
                    _movementX = wayPoints[_wayPtIncrement + 1].position.x;
                    _movementY = transform.position.y;
                    _movementZ = wayPoints[_wayPtIncrement + 1].position.z;
                }
                
                transform.position = new Vector3(_movementX, _movementY, _movementZ);
                yield return new WaitForSeconds(1);

                if (Vector3.Distance(transform.position, wayPoints[_wayPtIncrement + 1].position) <= _thresholdInWayPt)
                {
                    Debug.Log("I am in rot");
                    
                    _rotation = new Vector3(transform.eulerAngles.x, wayPoints[_wayPtIncrement + 1].eulerAngles.y, transform.eulerAngles.z);
 
                    transform.rotation = Quaternion.Euler(_rotation);
                }
                _wayPtIncrement++;
                
            }
        }
        
    }
}
