using Managers;
using UnityEngine;

namespace Controllers
{
    public class EndLevelView : MonoBehaviour
    {
        // private GameObject _endLevelButton;
        // public void SetActiveTrue()
        // {
        //     _endLevelButton.SetActive(true);
        // }
        // public void SetActiveFalse()
        // {
        //     _endLevelButton.SetActive(false);
        // }
        public void EndLevelButton()
        {
            MenuManager.Instance.EndLevel();
        }
    }
}
