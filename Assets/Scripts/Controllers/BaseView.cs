using UnityEngine;

namespace Controllers
{
    public class BaseView : MonoBehaviour
    {
        public void ShowView() => gameObject.SetActive(true);

        public virtual void HideView() => gameObject.SetActive(false);
    }
}
