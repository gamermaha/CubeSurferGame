using UnityEngine;

namespace Controllers
{
    public class CameraConfig : MonoBehaviour
    {
        [SerializeField] private Camera _playerCamera;
        private Vector3 _cameraPos;
        private Quaternion _cameraAngle;

        void Update()
        {
            _cameraPos = _playerCamera.transform.position;
            _cameraAngle = _playerCamera.transform.rotation;

        }
    }
}
