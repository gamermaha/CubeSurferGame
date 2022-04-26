
using Managers;
using UnityEngine;

namespace Controllers
{
    public class GameOverView : MonoBehaviour
    {
        // public void SetActiveTrue()
        // {
        //     gameObject.SetActive(true);
        // }
        // public void SetActiveFalse()
        // {
        //     gameObject.SetActive(false);
        // }
        public void GameOverButton()
        {
            MenuManager.Instance.RestartOnGameOverButton();
        }
    }
    
}
