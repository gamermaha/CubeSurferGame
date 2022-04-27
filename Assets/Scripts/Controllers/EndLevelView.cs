using Managers;
using UnityEngine;

namespace Controllers
{
    public class EndLevelView : MonoBehaviour
    {
        public void EndLevelButton() =>MenuManager.Instance.EndLevel();
    }
}
