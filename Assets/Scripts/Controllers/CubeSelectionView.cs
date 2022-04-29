using Managers;
using UnityEngine;

namespace Controllers
{
    public class CubeSelectionView : BaseView
    {
        public Material cubeColour;

        public void DiamondColButton()
        {
            cubeColour.color = Color.magenta;
        }

        public void YellowColButton()
        {
            cubeColour.color = Color.yellow;
        }

        public void BlueColButton()
        {
            cubeColour.color = Color.blue;
        }

        public void BackButton()
        {
            MenuManager.Instance.ChangeCubeColourDisabled();
        }
    }
}
