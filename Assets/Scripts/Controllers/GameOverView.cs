
using Managers;
using UnityEngine;

namespace Controllers
{
    public class GameOverView : MonoBehaviour
    {
        public void GameOverButton() => MenuManager.Instance.RestartOnGameOverButton();
    }
    
}
