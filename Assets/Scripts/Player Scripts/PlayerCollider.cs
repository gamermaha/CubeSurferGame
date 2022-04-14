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
        
        [SerializeField] private PlayerController player;
       private double _cubeSize;
       
       private void Start()
        {
            _cubeSize = MetaData.Instance.scriptableInstance.cubeLength;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Cube"))
            {
                player.AddCube(other.gameObject);
                transform.localScale += new Vector3(0f, (float) _cubeSize, 0f);
            }
            else if (other.gameObject.CompareTag("CubeDestroy"))
            {
                DestroyCubeCalled = true;
                cubeToDestroyScripts = other.gameObject.GetComponentsInChildren<CubeToDestroy>();
                
                Vector3 playerLocalPos = player.transform.GetChild(0).localPosition;
                Debug.Log(player.transform.GetChild(0).localPosition);
                
                 if (cubeToDestroyScripts.Length == 3)
                 {
                     // Debug.Log("Obstacle size is " + cubeToDestroyScripts[0].obstacleSize);
                     // Debug.Log("Obstacle size is " + cubeToDestroyScripts[1].obstacleSize);
                     // Debug.Log("Obstacle size is " + cubeToDestroyScripts[2].obstacleSize);

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
                
                transform.localScale -= new Vector3(0f, (float) _cubeSize, 0f);
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
        }
    }
}
