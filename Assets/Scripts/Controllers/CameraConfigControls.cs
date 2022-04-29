using System;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class CameraConfigControls : MonoBehaviour
    {
        public Slider xTransSlider;
        public Slider yTransSlider;
        public Slider zTransSlider;
        public Slider xRotSlider;
        public Slider yRotSlider;
        
        private Camera _cam;
        private float _xTransSliderDefValue;
        private float _yTransSliderDefValue;
        private float _zTransSliderDefValue;
        private float _xRotSliderDefValue;
        private float _yRotSliderDefValue;

        void Start()
        {
            _cam = FindObjectOfType<Camera>();
            
            xTransSlider.minValue = MetaData.Instance.scriptableInstance.xTransSliderMinValue ;
            xTransSlider.maxValue = MetaData.Instance.scriptableInstance.xTransSliderMaxValue;
            yTransSlider.minValue = MetaData.Instance.scriptableInstance.yTransSliderMinValue;
            yTransSlider.maxValue = MetaData.Instance.scriptableInstance.yTransSliderMaxValue;
            zTransSlider.minValue = MetaData.Instance.scriptableInstance.zTransSliderMinValue;
            zTransSlider.maxValue = MetaData.Instance.scriptableInstance.zTransSliderMaxValue;
            xRotSlider.minValue = MetaData.Instance.scriptableInstance.xRotSliderMinValue;
            xRotSlider.maxValue = MetaData.Instance.scriptableInstance.xRotSliderMaxValue;
            yRotSlider.minValue = MetaData.Instance.scriptableInstance.yRotSliderMinValue;
            yRotSlider.maxValue = MetaData.Instance.scriptableInstance.yRotSliderMaxValue;
            
            _xTransSliderDefValue = MetaData.Instance.scriptableInstance.xTransSliderDefValue;
            _yTransSliderDefValue = MetaData.Instance.scriptableInstance.yTransSliderDefValue;
            _zTransSliderDefValue = MetaData.Instance.scriptableInstance.zTransSliderDefValue;
            _xRotSliderDefValue = MetaData.Instance.scriptableInstance.xRotSliderDefValue;
            _yRotSliderDefValue = MetaData.Instance.scriptableInstance.yRotSliderDefValue;
            
            

            xTransSlider.value = PlayerPrefs.GetFloat("CamxValue", _xTransSliderDefValue);
            yTransSlider.value = PlayerPrefs.GetFloat("CamyValue", _yTransSliderDefValue);
            zTransSlider.value = PlayerPrefs.GetFloat("CamzValue", _zTransSliderDefValue);
            xRotSlider.value = PlayerPrefs.GetFloat("CamxRot", _xRotSliderDefValue);
            yRotSlider.value = PlayerPrefs.GetFloat("CamyRot", _yRotSliderDefValue);
        }

        void Update()
        {
            _cam.transform.localPosition = new Vector3(xTransSlider.value, yTransSlider.value, zTransSlider.value);
            _cam.transform.localRotation = Quaternion.Euler(xRotSlider.value, yRotSlider.value, 0f);
        }
        
        public void SaveCameraValues()
        {
            PlayerPrefs.SetFloat("CamxValue", xTransSlider.value);
            PlayerPrefs.SetFloat("CamyValue", yTransSlider.value);
            PlayerPrefs.SetFloat("CamzValue", zTransSlider.value);
            PlayerPrefs.SetFloat("CamxRot", xRotSlider.value);
            PlayerPrefs.SetFloat("CamyRot", yRotSlider.value);
        }

        public void BackButton() => GameManager.Instance.BackFromDebugScene();
    }
    
}
