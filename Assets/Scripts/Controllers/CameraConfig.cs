using UnityEngine;

namespace Controllers
{
    public class CameraConfig : MonoBehaviour
    {
        private float _xValue;
        private float _yValue;
        private float _zValue;
        private float _xRot;
        private float _yRot;
        
        void Start()
        {
            _xValue = PlayerPrefs.GetFloat("CamxValue", -3);
            _yValue = PlayerPrefs.GetFloat("CamyValue", -1);
            _zValue = PlayerPrefs.GetFloat("CamzValue", -75);
            _xRot= PlayerPrefs.GetFloat("CamxRot", -45);
            _yRot = PlayerPrefs.GetFloat("CamyRot", -45);
            
            transform.position = new Vector3(_xValue, _yValue, _zValue);
            transform.rotation = Quaternion.Euler(_xRot, _yRot, 0f);
        }
    }
}
