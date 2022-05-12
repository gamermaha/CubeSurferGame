using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class EndLevelView : BaseView
    {
        public Text diamondCountDisplay;
        [SerializeField] private GameObject confetti; 
        public void ShowDiamondCount(int diamondCount) => diamondCountDisplay.text = "Diamonds Collected: " + diamondCount;

        public void ShowConfetti(Vector3 confettiPos)
        {
            confetti.SetActive(true);
            confetti.transform.position = confettiPos;
        }

        public override void HideView()
        {
            base.HideView();
            confetti.SetActive(false);
        }
        
        public void EndLevelButton() => MenuManager.Instance.EndLevel();
    }
}
