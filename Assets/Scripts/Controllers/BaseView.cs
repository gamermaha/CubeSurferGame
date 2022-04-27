using UnityEngine;

namespace Controllers
{
    public class BaseView : MonoBehaviour
    {
        public void ShowView() => gameObject.SetActive(true);

        public void HideView() => gameObject.SetActive(false);
    }
}
