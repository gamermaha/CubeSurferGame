using System;
using UnityEngine;

namespace Controllers
{
    public class InputClass : MonoBehaviour
    {
        private Vector3 prevMousePos;
        void Awake()
        {
            Debug.Log("I am awake");
            prevMousePos = new Vector3(0f, 0f, 0f);
        }
        void Update()
        {
            transform.position += new Vector3(0f, 0f, 0.1f) * (10 * Time.deltaTime);

            if (transform.position.x <= 4f && transform.position.x >= -4f)
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
            else if (transform.position.x > 4f)
            { 
                if (Input.GetMouseButton(0) && (Input.mousePosition.x - prevMousePos.x) < 0)
                {
                    MoveLeft();
                }
            }
            else if (transform.position.x < -4f)
            {
                if (Input.GetMouseButton(0) && (Input.mousePosition.x - prevMousePos.x) > 0)
                {
                    MoveRight();
                }
            }
        }

        private void MoveRight()
        {
            Debug.Log("Moving to the right");
            prevMousePos = Input.mousePosition;
            transform.Translate(0.1f, 0f, 0f);
        }
        
        private void MoveLeft()
        {
            Debug.Log("Moving to the left");
            prevMousePos = Input.mousePosition;
            transform.Translate(-0.1f, 0f, 0f);
        }
    } 
}

