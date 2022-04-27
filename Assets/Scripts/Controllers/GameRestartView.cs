using Managers;
using UnityEngine;

namespace Controllers
{
    public class GameRestartView : MonoBehaviour
    {
        public void NewLevelButton() => MenuManager.Instance.PlayGame();
    }
}
