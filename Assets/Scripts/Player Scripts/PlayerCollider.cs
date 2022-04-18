using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Managers;
using UnityEngine;
using UnityEngine.Serialization;


namespace Player_Scripts
{
    public class PlayerCollider : MonoBehaviour
    { 
        [SerializeField] private GameObject playerForMagnet;
        [SerializeField] private Magnet magnetCollider;
        public static bool DestroyCubeCalled;
        public CubeToDestroy[] cubeToDestroyScripts;
        
        
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
            if (other.gameObject.CompareTag("Cube"))
            {
                player.AddCube(other.gameObject);
                
            }
            else if (other.gameObject.CompareTag("CubeDestroy"))
            {
                DestroyCubeCalled = true;
                cubeToDestroyScripts = other.gameObject.GetComponentsInChildren<CubeToDestroy>();
                
                Vector3 playerLocalPos = player.transform.GetChild(0).localPosition;
                //Debug.Log(player.transform.GetChild(0).localPosition);
                transform.localScale -= new Vector3(0f, (float) _cubeSize, 0f);
                 if (cubeToDestroyScripts.Length == 3)
                 {
                     if (playerLocalPos.x >= -3f && playerLocalPos.x < -1f)
                     {
                         Debug.Log("Obstacle size is " + cubeToDestroyScripts[0].obstacleSize);
                         player.DestroyCube(other.gameObject, cubeToDestroyScripts[0].obstacleSize);
                         
                     }
                     else if (playerLocalPos.x >= -1f && playerLocalPos.x <= 1f)
                     {
                         Debug.Log("Obstacle size is " + cubeToDestroyScripts[1].obstacleSize);
                         player.DestroyCube(other.gameObject, cubeToDestroyScripts[1].obstacleSize);
                     }
                     else if (playerLocalPos.x > 1f && playerLocalPos.x <= 3f)
                     {
                         Debug.Log("Obstacle size is " + cubeToDestroyScripts[2].obstacleSize);
                         player.DestroyCube(other.gameObject, cubeToDestroyScripts[2].obstacleSize);
                     }
                     
                 }
                 else
                 {
                     player.DestroyCube(other.gameObject, cubeToDestroyScripts[0].obstacleSize);
                 }
                 
            }
            else if (other.gameObject.CompareTag("Diamond"))
            {
                player.AddDiamond(other.gameObject);
            }
            else if (other.gameObject.CompareTag("EndLadder"))
            {
                player.EndLadder(other.gameObject);
            }
            else if (other.gameObject.CompareTag("EndLevel"))
            {
                player.EndLevel(other.gameObject);
            }
            else if (other.gameObject.CompareTag("WaterObstacle"))
            {
                player.WaterObstacle();
            }
            else if (other.gameObject.CompareTag("Magnet"))
            {
                other.tag = "MagnetGrabbed";
                Magnet magnetCol = Instantiate(magnetCollider);
                magnetCol.transform.SetParent(player.transform.GetChild(0));
                player.magnetSprite = other.gameObject;
                //player.MagnetAnimationCall();
                //player.magnetEnabled = true;
                //Debug.Log("MagnetEnabled is set to true");
                //other.transform.position = playerForMagnet.transform.position;
                //player.MagnetAnimationCall(other.gameObject.transform.position);
                
                
                Destroy(magnetCol, _destroyMagnetTime);
                Destroy(other.gameObject, _destroyMagnetTime);
            }
            else if (other.gameObject.CompareTag("DiamondMultiplier"))
            {
                player.diamondMulti = true;
                player.DiamondMultiAnimation(other.gameObject.transform.position);
                Destroy(other.gameObject);
            }
        }
    }
}
