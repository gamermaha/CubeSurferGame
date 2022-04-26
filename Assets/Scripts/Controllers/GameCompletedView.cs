using Managers;
using UnityEngine;

namespace Controllers
{
    public class GameCompletedView : MonoBehaviour
    {
        // private GameObject _gameCompletedButton;
        // public void SetActiveTrue()
        // {
        //     _gameCompletedButton.SetActive(true);
        // }
        // public void SetActiveFalse()
        // {
        //     _gameCompletedButton.SetActive(false);
        // }
        public void GameCompletedButton()
        {
            MenuManager.Instance.GameCompleted();
        }
    }
}
