using Managers;
using UnityEngine;

namespace Controllers
{
    public class GameCompletedView : MonoBehaviour
    {
        public void GameCompletedButton() => MenuManager.Instance.GameCompleted();
    }
}
