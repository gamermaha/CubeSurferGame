using UnityEngine;

namespace Player_Scripts
{
    public class CubeToDestroy : MonoBehaviour
    {
        // Start is called before the first frame update
        public float obstacleSize;
        void Start()
        {
            obstacleSize = transform.localScale.y;
        }
    }
}
