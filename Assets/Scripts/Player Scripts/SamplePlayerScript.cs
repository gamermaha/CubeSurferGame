using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player_Scripts
{
    public class SamplePlayerScript : MonoBehaviour
    {

        [SerializeField] private List<Transform> wayPoints;

        private float _mySpeed = 0.5f;
        private float _moveForce = 10f;
        private int _x = 1;
        private int _wayPtIncrement = 0;
        private float _thresholdInWayPt = 0.5f;
        private Vector3 _rotation;
        private float _movementX;
        private float _movementY;
        private float _movementZ;


        private Vector3 _lastPos;
        private Vector3 _currentPos;
        private Vector3 _prevMousePos;
        private Vector3 _offsetVector;
        


        private void Start()
        {
            _lastPos = transform.position;
        }

        void Update()
        {

            //StartCoroutine(PlayerMovement());

            float distance = Vector3.Distance(wayPoints[_wayPtIncrement].position, transform.position);
            transform.position =
                Vector3.MoveTowards(transform.position, wayPoints[_wayPtIncrement].position, Time.deltaTime);

            var rotation = Quaternion.LookRotation(wayPoints[_wayPtIncrement].position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);

            if (distance <= _thresholdInWayPt)
                _wayPtIncrement++;
            if (_wayPtIncrement >= wayPoints.Count)
                _wayPtIncrement = 0;
            
            OnCenter();


        }

        // IEnumerator PlayerMovement()
        // {
        //     while (_wayPtIncrement + 1 < wayPoints.Count)
        //     {
        //         if (Math.Abs(transform.position.x - wayPoints[_wayPtIncrement + 1].position.x) != 0f)
        //         {
        //             Debug.Log("I am in x change");
        //             _movementX = wayPoints[_wayPtIncrement + 1].position.x;
        //             _movementY = transform.position.y;
        //             _movementZ = transform.position.z;
        //         }
        //         else if (Math.Abs(transform.position.z - wayPoints[_wayPtIncrement + 1].position.z) != 0f)
        //         {
        //             Debug.Log("I am in z change");
        //             _movementX = transform.position.x;
        //             _movementY = transform.position.y;
        //             _movementZ = wayPoints[_wayPtIncrement + 1].position.z;
        //         }
        //         else if (Math.Abs(transform.position.z - wayPoints[_wayPtIncrement + 1].position.z) != 0f &&
        //                  Math.Abs(transform.position.x - wayPoints[_wayPtIncrement + 1].position.x) != 0f)
        //         {
        //             Debug.Log("I am in both change");
        //             _movementX = wayPoints[_wayPtIncrement + 1].position.x;
        //             _movementY = transform.position.y;
        //             _movementZ = wayPoints[_wayPtIncrement + 1].position.z;
        //         }
        //
        //         transform.position = Vector3.MoveTowards(transform.position,
        //             new Vector3(_movementX, _movementY, _movementZ), (_mySpeed * Time.deltaTime)) ;
        //
        //         yield return new WaitForSeconds(1f);
        //         if (_wayPtIncrement + 1 < wayPoints.Count)
        //         {
        //             if (Vector3.Distance(transform.position, wayPoints[_wayPtIncrement + 1].position) <=
        //                 _thresholdInWayPt)
        //             {
        //
        //                 _rotation = new Vector3(transform.eulerAngles.x, wayPoints[_wayPtIncrement + 1].eulerAngles.y,
        //                     transform.eulerAngles.z);
        //
        //                 transform.rotation = Quaternion.Euler(_rotation);
        //                 _wayPtIncrement++;
        //             }
        //
        //         }
        //
        //     }
        //     // transform.position += new Vector3(_movementX, _movementY, _movementZ) * (_moveForce * _mySpeed * Time.deltaTime);
        // }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Cube"))
                Destroy(other.gameObject);
                
        }

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

        private void MoveRight()
        {
            // Debug.Log("Moving to the right");
            _prevMousePos = Input.mousePosition;
            //Mathf.Clamp(transform.GetChild(0).localPosition.x, -3, 3); 
            //transform.GetChild(0).Translate(0.1f, 0f, 0f);
            // _offsetVector = new Vector3(Time.deltaTime, 0f, 0f);
            // transform.GetChild(0).localPosition += _offsetVector;
            transform.GetChild(0).localPosition = new Vector3(Mathf.Clamp(transform.GetChild(0).localPosition.x + 0.1f, transform.position.x-3, transform.position.x+3), 0f, 0f);
        }

        private void MoveLeft()
        {
            // Debug.Log("Moving to the left");
            _prevMousePos = Input.mousePosition;
            transform.GetChild(0).Translate(-0.1f, 0f, 0f);
            //_offsetVector = new Vector3(Time.deltaTime, 0f, 0f);
            transform.GetChild(0).localPosition = new Vector3(Mathf.Clamp(transform.GetChild(0).localPosition.x - 0.1f, transform.position.x-3, transform.position.x+3), 0f, 0f);
        }
    }


}
