using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class CubeSelectionView : BaseView
    {
        public Material cubeColour;
        public RawImage cubeImage;
        public Image cubeImgOnCubeSelButton;
        
        public void DiamondColButton()
        {
            cubeColour.color = Color.magenta;
            cubeImage.color = Color.magenta;
            cubeImgOnCubeSelButton.color = Color.magenta;
            
        }

        public void YellowColButton()
        {
            cubeColour.color = Color.yellow;
            cubeImage.color = Color.yellow;
            cubeImgOnCubeSelButton.color = Color.yellow;
        }

        public void BlueColButton()
        {
            cubeColour.color = Color.blue;
            cubeImage.color = Color.blue;
            cubeImgOnCubeSelButton.color = Color.blue;
        }
        
        public void CyanColButton()
        {
            cubeColour.color = Color.cyan;
            cubeImage.color = Color.cyan;
            cubeImgOnCubeSelButton.color = Color.cyan;
        }
        
        public void RedColButton()
        {
            cubeColour.color = Color.red;
            cubeImage.color = Color.red;
            cubeImgOnCubeSelButton.color = Color.red;
            
        }
        
        public void GreyColButton()
        {
            cubeColour.color = Color.grey;
            cubeImage.color = Color.grey;
            cubeImgOnCubeSelButton.color = Color.grey;
        }
        
        public void BackButton() => MenuManager.Instance.ChangeCubeColourDisabled();
    }
}
