using UnityEngine;

namespace Controllers
{
    public class BaseView : MonoBehaviour
    {
        public void ShowView(GameObject viewToShow) => viewToShow.SetActive(true);

        public void HideView(GameObject viewToHide) => viewToHide.SetActive(false);
    }
}
