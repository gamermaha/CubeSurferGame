using System.Collections;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{ 
    public class GameStartView : BaseView
    {
        public Slider handSlider;
        public Image cubeImgOnChangeColButton;
        public Material cubeMaterial;
        public Material trailMaterial;

        private string _cubeCol;

        private void OnEnable()
        {
            handSlider.value = 0;
            _cubeCol = PlayerPrefs.GetString("CubeColor", "yellow");
            switch (_cubeCol)
            {
                case "magenta":
                    cubeImgOnChangeColButton.color = Color.magenta;
                    cubeMaterial.color = Color.magenta;
                    trailMaterial.color = Color.magenta;
                    break;
                case "blue":
                    cubeImgOnChangeColButton.color = Color.blue;
                    cubeMaterial.color = Color.blue;
                    trailMaterial.color = Color.blue;
                    break;
                case "cyan":
                    cubeImgOnChangeColButton.color = Color.cyan;
                    cubeMaterial.color = Color.cyan;
                    trailMaterial.color = Color.cyan;
                    break;
                case "grey":
                    cubeImgOnChangeColButton.color = Color.gray;
                    cubeMaterial.color = Color.gray;
                    trailMaterial.color = Color.gray;
                    
                    break;
                case "yellow":
                    cubeImgOnChangeColButton.color = Color.yellow;
                    cubeMaterial.color = Color.yellow;
                    trailMaterial.color = Color.yellow;
                    break;
                case "red":
                    cubeImgOnChangeColButton.color = Color.red;
                    cubeMaterial.color = Color.red;
                    trailMaterial.color = Color.red;
                    break;
            }
            StartCoroutine(HandSlider());
        }
        
        public override void HideView()
        {
            base.HideView();
            StopCoroutine(HandSlider());
        }

        public IEnumerator HandSlider()
        {
            while (handSlider.value < 2f)
            {
                yield return new WaitForSeconds(0.04f);
                handSlider.value += 0.01f;
                if (handSlider.value >= 1)
                    handSlider.value = 0;
            }
        }

        public void GameStartButton() => MenuManager.Instance.PlayGame();
        
        public void ChangeCubeColButton() => MenuManager.Instance.ChangeCubeColourEnabled();
    }
}
