using Managers;
using UnityEngine;

namespace Player_Scripts
{
    public class PlayerCollider : MonoBehaviour
    {
        public static bool DESTROYCUBECALLED;
        public CubeToDestroy[] cubeToDestroyScripts;
        
        [SerializeField] private Magnet magnetCollider;
        [SerializeField] private PlayerController player;
        
        private double _cubeSize; 
        private float _destroyMagnetTime;
        
        private void Start()
        {
            _cubeSize = MetaData.Instance.scriptableInstance.cubeLength;
            _destroyMagnetTime = MetaData.Instance.scriptableInstance.destroyMagnetTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            string collidedTag = other.gameObject.tag;
            
            switch (collidedTag)
            {
                case "Cube":
                    player.AddCube(other.gameObject);
                    break;
                case "CubeDestroy":
                {
                    DESTROYCUBECALLED = true;
                    cubeToDestroyScripts = other.gameObject.GetComponentsInChildren<CubeToDestroy>(); 
                    Vector3 playerLocalPos = player.transform.GetChild(0).localPosition;
                    int increment = 0;
                        
                    if (cubeToDestroyScripts.Length == 3)
                    {
                        if (playerLocalPos.x >= -3f && playerLocalPos.x < -1f)
                            increment = 0;
                        else if (playerLocalPos.x >= -1f && playerLocalPos.x <= 1f)
                            increment = 1;
                        else if (playerLocalPos.x > 1f && playerLocalPos.x <= 3f)
                            increment = 2;
                    }
                    else 
                        increment = 0;
                        
                    player.DestroyCube(other.gameObject, cubeToDestroyScripts[increment].obstacleSize);
                    transform.localScale -= new Vector3(0f, (float) _cubeSize, 0f);
                }
                    break;
                case "Diamond":
                    player.AddDiamond(other.gameObject);
                    break;
                case "EndLadder":
                    player.EndLadder(other.gameObject);
                    break;
                case "EndLevel":
                    player.EndLevel(other.gameObject);
                    break;
                case "WaterObstacle":
                    player.WaterObstacle();
                    break;
                case "Magnet":
                player.MagnetCollected(other.gameObject);
                    break;
                case "DiamondMultiplier":
                    player.DiamondMulti(other.gameObject);
                    break;
            }
        }
    }
}
