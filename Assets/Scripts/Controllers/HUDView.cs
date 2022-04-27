﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Managers;

namespace Controllers
{
    public class HUDView : BaseView
    {
        public GameObject diamondSprite;
        public Text diamondCountDisplay;
        public Text times2;
        public Image hUDDiamondImage;
        public Slider levelProgression;
        public GameObject settingsContainer;

        private Image _settingsContainerImg;
        private Color _settingsContainerColor;


        private void Start()
        {
            levelProgression.value = 0;
            _settingsContainerImg = settingsContainer.GetComponent<Image>();
            _settingsContainerColor = _settingsContainerImg.color;
            _settingsContainerColor.a = 255;

        }
       
        public void UpdateDiamondCount(int diamondCount) => diamondCountDisplay.text = "" + diamondCount;
        
        public void DiamondAnimation(Vector3 instantiatePos, Camera cam)
        {
            GameObject diamond = Instantiate(diamondSprite);
            diamond.transform.SetParent(transform);
            diamond.transform.position = cam.WorldToScreenPoint(instantiatePos);
            diamond.transform.DOMove(hUDDiamondImage.transform.position, 0.35f)
                .SetEase(Ease.Unset).OnComplete(() =>
                {
                    Destroy(diamond);
                });
        }
        public void DiamondAnimationTimesTwo(string display)
        {
            times2.transform.position = hUDDiamondImage.transform.position;
            times2.text = display;
        }
        public void SliderUpdate(float sliderValue) => levelProgression.value = sliderValue;

        public void SettingsContainer()
        {
            if (settingsContainer.activeSelf)
            {
                settingsContainer.SetActive(false);
            }
            else
            { 
                settingsContainer.SetActive(true);
            }
        }

        private IEnumerator SettingsContainerFader()
        {
            while (_settingsContainerColor.a >= 0)
            {
                //settingsContainer.SetActive(false);
                yield return new WaitForSeconds(0.1f);
                _settingsContainerColor.a -= 5f;
            }
            
        }

        public void CameraConfigButton() => GameManager.Instance.LoadDebugScene();

        public void AudioOnOff()
        {
            AudioManager.Instance.SetOnOff();
        }
    }
}
