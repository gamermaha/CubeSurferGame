using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class CubeSelectionView : BaseView
    {
        public Material cubeColour;
        public Image cubeImage;

        public void DiamondColButton()
        {
            cubeColour.color = Color.magenta;
            cubeImage.color = Color.magenta;
        
        }

        public void YellowColButton()
        {
            cubeColour.color = Color.yellow;
            cubeImage.color = Color.yellow;
        }

        public void BlueColButton()
        {
            cubeColour.color = Color.blue;
            cubeImage.color = Color.blue;
        
        }

        public void BackButton()
        {
            MenuManager.Instance.ChangeCubeColourDisabled();
        }
    }
}
