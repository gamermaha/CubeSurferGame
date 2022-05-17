using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class CubeSelectionView : BaseView
    {
        public Material cubeColour;
        public Material trailColor;
        public RawImage cubeImage;
        public Image cubeImgOnCubeSelButton;
        
        public void DiamondColButton()
        {
            cubeColour.color = Color.magenta;
            trailColor.color = Color.magenta;
            cubeImage.color = Color.magenta;
            cubeImgOnCubeSelButton.color = Color.magenta;
            PlayerPrefs.SetString("CubeColor", "magenta");
            
        }

        public void YellowColButton()
        {
            cubeColour.color = Color.yellow;
            trailColor.color = Color.yellow;
            cubeImage.color = Color.yellow;
            cubeImgOnCubeSelButton.color = Color.yellow;
            PlayerPrefs.SetString("CubeColor", "yellow");
        }

        public void BlueColButton()
        {
            cubeColour.color = Color.blue;
            trailColor.color = Color.blue;
            cubeImage.color = Color.blue;
            cubeImgOnCubeSelButton.color = Color.blue;
            PlayerPrefs.SetString("CubeColor", "blue");
        }
        
        public void CyanColButton()
        {
            cubeColour.color = Color.cyan;
            trailColor.color = Color.cyan;
            cubeImage.color = Color.cyan;
            cubeImgOnCubeSelButton.color = Color.cyan;
            PlayerPrefs.SetString("CubeColor", "cyan");
        }
        
        public void RedColButton()
        {
            cubeColour.color = Color.red;
            trailColor.color = Color.red;
            cubeImage.color = Color.red;
            cubeImgOnCubeSelButton.color = Color.red;
            PlayerPrefs.SetString("CubeColor", "red");
            
        }
        
        public void GreyColButton()
        {
            cubeColour.color = Color.grey;
            trailColor.color = Color.grey;
            cubeImage.color = Color.grey;
            cubeImgOnCubeSelButton.color = Color.grey;
            PlayerPrefs.SetString("CubeColor", "grey");
        }
        
        public void BackButton() => MenuManager.Instance.ChangeCubeColourDisabled();
    }
}
