using UnityEngine;

namespace Player_Scripts
{
    public class PlayerChild : MonoBehaviour
    {
        void OnTriggerExit(Collider collision)
        { 
            gameObject.GetComponentInParent<PlayerController>().PullTrigger(collision);
        }
    }
}
