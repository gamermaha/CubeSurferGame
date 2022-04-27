using Managers;

namespace Controllers
{
    public class EndLevelView : BaseView
    {
        public void EndLevelButton() =>MenuManager.Instance.EndLevel();
    }
}
