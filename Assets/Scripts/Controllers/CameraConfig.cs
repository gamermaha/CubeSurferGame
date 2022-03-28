using UnityEngine;

namespace Controllers
{
    public class CameraConfig : MonoBehaviour
    {
        private Vector3 _cameraPos;
        private Quaternion _cameraAngle;

        void Update()
        {
            _cameraPos = transform.position;
            _cameraAngle = transform.rotation;

        }
    }
}
