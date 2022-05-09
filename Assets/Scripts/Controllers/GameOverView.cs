using Managers;

namespace Controllers
{
    public class GameOverView : BaseView
    { 
        public void GameOverButton() => MenuManager.Instance.RestartOnGameOverButton();
    }
}
