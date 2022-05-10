using System.Collections;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{ 
    public class GameStartView : BaseView
    {
        public Slider handSlider;

        private void Start()=> handSlider.value = 0;

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

        public void GameStartButton()
        {
            MenuManager.Instance.PlayGame();
        }
        
        public void ChangeCubeColButton()
        {
            MenuManager.Instance.ChangeCubeColourEnabled();
        }
    }
}
