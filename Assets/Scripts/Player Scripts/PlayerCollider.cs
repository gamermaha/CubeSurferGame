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
        [SerializeField] private PlayerController player;
       private double _cubeSize;
       public static bool DestroyCubeCalled;
       public CubeToDestroy[] cubeToDestroyScripts; 
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
                player.DestroyCube(other.gameObject, cubeToDestroyScripts[0].obstacleSize);
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
