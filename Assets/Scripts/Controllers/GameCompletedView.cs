using Managers;

namespace Controllers
{
    public class GameCompletedView : BaseView
    {
        public void GameCompletedButton() => MenuManager.Instance.GameCompleted();
    }
}
