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

        private string _cubeCol;

        private void OnEnable()
        {
            handSlider.value = 0;
            _cubeCol = PlayerPrefs.GetString("CubeColor", "yellow");
            switch (_cubeCol)
            {
                case "magenta":
                    cubeImgOnChangeColButton.color = Color.magenta;
                    break;
                case "blue":
                    cubeImgOnChangeColButton.color = Color.blue;
                    break;
                case "cyan":
                    cubeImgOnChangeColButton.color = Color.cyan;
                    break;
                case "grey":
                    cubeImgOnChangeColButton.color = Color.gray;
                    break;
                case "yellow":
                    cubeImgOnChangeColButton.color = Color.yellow;
                    break;
                case "red":
                    cubeImgOnChangeColButton.color = Color.red;
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
