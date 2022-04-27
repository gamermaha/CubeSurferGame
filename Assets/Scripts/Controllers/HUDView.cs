using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Managers;

namespace Controllers
{
    public class HUDView : MonoBehaviour
    {
        public GameObject diamondSprite;
        public Text diamondCountDisplay;
        public Text times2;
        public Image hUDDiamondImage;
        public Slider levelProgression;

        private void Start() => levelProgression.value = 0;
       
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

        public void CameraConfigButton() => GameManager.Instance.LoadDebugScene();
    }
}
