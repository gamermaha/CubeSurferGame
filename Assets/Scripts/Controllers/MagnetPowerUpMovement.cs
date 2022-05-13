using UnityEngine;

namespace Controllers
{
    public class MagnetPowerUpMovement : MonoBehaviour
    {
        void Update() => transform.Rotate(0f, 40f * Time.deltaTime, 40f* Time.deltaTime);
        
    }
}
