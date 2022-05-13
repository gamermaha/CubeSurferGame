using UnityEngine;

namespace Controllers
{
    public class BoysMovement : MonoBehaviour
    {

        void Update() => transform.Rotate(0f,45f* Time.deltaTime, 0f);
    }
}
