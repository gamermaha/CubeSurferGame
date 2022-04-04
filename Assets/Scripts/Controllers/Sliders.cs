using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class Sliders : MonoBehaviour
    {
        public Slider slider;
        private float _playerProgress;
        //private float value;

        public void OnSliderChanged()
        {
            _playerProgress++;
            slider.value = _playerProgress;
            Debug.Log(slider.value);
        }

        public void SliderValueSetter(float playerProg)
        {
            _playerProgress = playerProg;
        }
    }
}
