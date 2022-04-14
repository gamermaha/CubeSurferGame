using UnityEngine;

namespace Controllers
{
    public class ScreentoWorldSpace : MonoBehaviour
    {
        public Transform target;
        public Camera cam;

        void Start()
        {
            cam = GetComponentInChildren<Camera>();
        }

        void Update()
        {
            Vector3 screenPos = cam.WorldToScreenPoint(target.position);
            Debug.Log("target is " + screenPos.x + " pixels from the left");
        }
    }
}
 