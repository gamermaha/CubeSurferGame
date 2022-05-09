using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class GameCompletedView : BaseView
    {
        public void GameCompletedButton() => MenuManager.Instance.GameCompleted();
    }
}
