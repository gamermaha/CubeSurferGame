using Environment_Setters;
using UnityEngine;

namespace Player_Scripts
{
    public class PlayerCollider : MonoBehaviour
    {
        public CubeToDestroy[] cubeToDestroyScripts;
        
        [SerializeField] private PlayerController player;
        
        private void OnTriggerEnter(Collider other)
        {
            string collidedTag = other.gameObject.tag;
            
            switch (collidedTag)
            {
                case Constants.TAG_CUBE:
                    player.AddCube(other.gameObject);
                    break;
                case Constants.TAG_DESTROYCUBE:
                    TriggerCallForDestroyCube(other);
                    break;
                case Constants.TAG_DIAMOND:
                    player.AddDiamond(other.gameObject);
                    break;
                case Constants.TAG_ENDLADDER:
                    player.EndLadder(other.gameObject, 1f);
                    break;
                case Constants.TAG_ENDLEVEL:
                    player.EndLadder(other.gameObject, 1f);
                    break;
                case Constants.TAG_WATEROBSTACLE:
                    player.WaterObstacle();
                    break;
                case Constants.TAG_MAGNET:
                player.MagnetCollected(other.gameObject);
                    break;
                case Constants.TAG_DIAMONDMULTIPLIER:
                    player.DiamondMulti(other.gameObject);
                    break;
            }
        }

        private void TriggerCallForDestroyCube(Collider other)
        {
            cubeToDestroyScripts = other.gameObject.GetComponentsInChildren<CubeToDestroy>(); 
            Vector3 playerLocalPos = player.transform.GetChild(0).localPosition;
            int increment = 0;
                        
            if (cubeToDestroyScripts.Length > 3)
            {
                if (playerLocalPos.x >= -3f && playerLocalPos.x < -1f)
                    increment = 2;
                else if (playerLocalPos.x >= -1f && playerLocalPos.x < 1f)
                    increment = 1;
                else if (playerLocalPos.x >= 1f && playerLocalPos.x <= 3f)
                    increment = 0;
            }
            else 
                increment = 0;
                        
            player.DestroyCube(other.gameObject, cubeToDestroyScripts[increment].obstacleSize, increment);
        }
    }
}
