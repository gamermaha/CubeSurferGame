using Managers;
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
        
        private float _xTransSliderDefValue;
        private float _yTransSliderDefValue;
        private float _zTransSliderDefValue;
        private float _xRotSliderDefValue;
        private float _yRotSliderDefValue;
        
        void Start()
        {
            _xTransSliderDefValue = MetaData.Instance.scriptableInstance.xTransSliderDefValue;
            _yTransSliderDefValue = MetaData.Instance.scriptableInstance.yTransSliderDefValue;
            _zTransSliderDefValue = MetaData.Instance.scriptableInstance.zTransSliderDefValue;
            _xRotSliderDefValue = MetaData.Instance.scriptableInstance.xRotSliderDefValue;
            _yRotSliderDefValue = MetaData.Instance.scriptableInstance.yRotSliderDefValue;

            _xValue = PlayerPrefs.GetFloat("CamxValue", _xTransSliderDefValue);
            _yValue = PlayerPrefs.GetFloat("CamyValue", _yTransSliderDefValue);
            _zValue = PlayerPrefs.GetFloat("CamzValue", _zTransSliderDefValue);
            _xRot= PlayerPrefs.GetFloat("CamxRot", _xRotSliderDefValue);
            _yRot = PlayerPrefs.GetFloat("CamyRot", _yRotSliderDefValue);
            
            transform.position = new Vector3(_xValue, _yValue, _zValue);
            transform.rotation = Quaternion.Euler(_xRot, _yRot, 0f);
        }
    }
}
