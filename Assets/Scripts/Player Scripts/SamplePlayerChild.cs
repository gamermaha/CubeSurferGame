using UnityEngine;

namespace Player_Scripts
{
    public class SamplePlayerChild : MonoBehaviour
    {
        private Vector3 _prevMousePos;
        private Vector3 _offsetVector;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            OnCenter();
        }
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
            transform.Translate(0.1f, 0f, 0f);
            // _offsetVector += new Vector3(Time.deltaTime, 0f, 0f);
            // transform.localPosition += _offsetVector;
        }

        private void MoveLeft()
        {
            // Debug.Log("Moving to the left");
            _prevMousePos = Input.mousePosition;
            transform.Translate(-0.1f, 0f, 0f);
            // _offsetVector -= new Vector3(Time.deltaTime, 0f, 0f);
            // transform.localPosition -= _offsetVector;
        }
    }
}
