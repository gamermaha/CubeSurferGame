using System;
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

        void Start()
        {
            _cam = FindObjectOfType<Camera>();
            xTransSlider.minValue = -3;
            xTransSlider.maxValue = 10;
            yTransSlider.minValue = 1;
            yTransSlider.maxValue = 10;
            zTransSlider.minValue = -75;
            zTransSlider.maxValue = -25;
            xRotSlider.minValue = -45;
            xRotSlider.maxValue = 45;
            yRotSlider.minValue = -45;
            yRotSlider.maxValue = 45;

            xTransSlider.value = PlayerPrefs.GetFloat("CamxValue", xTransSlider.minValue);
            yTransSlider.value = PlayerPrefs.GetFloat("CamyValue", yTransSlider.minValue);
            zTransSlider.value = PlayerPrefs.GetFloat("CamzValue", zTransSlider.minValue);
            xRotSlider.value = PlayerPrefs.GetFloat("CamxRot", xRotSlider.minValue);
            yRotSlider.value = PlayerPrefs.GetFloat("CamyRot", yRotSlider.minValue);
        }

        void Update()
        {
            _cam.transform.position = new Vector3(xTransSlider.value, yTransSlider.value, zTransSlider.value);
            _cam.transform.rotation = Quaternion.Euler(xRotSlider.value, yRotSlider.value, 0f);
        }
        
        public void SaveCameraValues()
        {
            PlayerPrefs.SetFloat("CamxValue", xTransSlider.value);
            PlayerPrefs.SetFloat("CamyValue", yTransSlider.value);
            PlayerPrefs.SetFloat("CamzValue", zTransSlider.value);
            PlayerPrefs.SetFloat("CamxRot", xRotSlider.value);
            PlayerPrefs.SetFloat("CamyRot", yRotSlider.value);
        }
    }
    
}
