using Managers;

namespace Controllers
{
    public class GameRestartView : BaseView
    {
        public void NewLevelButton() => MenuManager.Instance.PlayGame();
    }
}
