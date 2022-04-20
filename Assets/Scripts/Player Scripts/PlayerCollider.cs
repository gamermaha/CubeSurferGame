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
        public static bool DestroyCubeCalled;
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
            if (other.gameObject.CompareTag("Cube"))
            {
                player.AddCube(other.gameObject);
                
            }
            else if (other.gameObject.CompareTag("CubeDestroy"))
            {
                DestroyCubeCalled = true;
                cubeToDestroyScripts = other.gameObject.GetComponentsInChildren<CubeToDestroy>();
                
                Vector3 playerLocalPos = player.transform.GetChild(0).localPosition;
                transform.localScale -= new Vector3(0f, (float) _cubeSize, 0f);

                int increment = 0;
                 if (cubeToDestroyScripts.Length == 3)
                 {
                     if (playerLocalPos.x >= -3f && playerLocalPos.x < -1f)
                     {
                         increment = 0;
                     }
                     else if (playerLocalPos.x >= -1f && playerLocalPos.x <= 1f)
                     {
                         increment = 1;
                     }
                     else if (playerLocalPos.x > 1f && playerLocalPos.x <= 3f)
                     {
                         increment = 2;
                     }
                     
                 }
                 else
                 {
                     increment = 0;
                 }
                 player.DestroyCube(other.gameObject, cubeToDestroyScripts[increment].obstacleSize);
                 
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
                AudioManager.Instance.PlaySounds(AudioManager.MAGNETCOLLECTEDSOUND);
                other.tag = "MagnetGrabbed";
                Magnet magnetCol = Instantiate(magnetCollider);
                magnetCol.transform.SetParent(player.transform.GetChild(0));
                player.magnetSprite = other.gameObject;
                
                Destroy(magnetCol, _destroyMagnetTime);
                Destroy(other.gameObject, _destroyMagnetTime);
            }
            else if (other.gameObject.CompareTag("DiamondMultiplier"))
            {
                AudioManager.Instance.PlaySounds(AudioManager.DIAMONDMULTIPLIERSOUND);
                player.diamondMulti = true;
                player.DiamondMultiAnimation("X2");
                Destroy(other.gameObject);
            }
        }
    }
}
