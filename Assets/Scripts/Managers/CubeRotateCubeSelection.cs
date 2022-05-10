using UnityEngine;

namespace Managers
{
    public class CubeRotateCubeSelection : MonoBehaviour
    { 
        void Update() => transform.Rotate(20f* Time.deltaTime, 20f * Time.deltaTime, 0f);
    }
}
