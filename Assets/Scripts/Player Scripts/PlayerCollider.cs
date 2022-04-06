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
            Debug.Log("@@@@@@@@@@@@@@@ End Level " + other.gameObject.name + ", " + other.gameObject.tag);
            if (other.gameObject.CompareTag("Cube"))
            {
                Debug.Log("I have encountered addition");
                player.AddCube(other.gameObject);
                transform.localScale += new Vector3(0f, (float) _cubeSize, 0f);
            }
            if (other.gameObject.CompareTag("CubeDestroy"))
            {
                DestroyCubeCalled = true;
                //Debug.Log("I have encountered: " + other.gameObject.GetComponentsInChildren<CubeToDestroy>(). + "cubes to destroy");
                cubeToDestroyScripts = other.gameObject.GetComponentsInChildren<CubeToDestroy>();
                //Debug.Log(cubeToDestroyScripts[0].obstacleSize);
                player.DestroyCube(other.gameObject, cubeToDestroyScripts[0].obstacleSize);
                transform.localScale -= new Vector3(0f, (float) _cubeSize, 0f);
            }
            else if (other.gameObject.CompareTag("EndLevel"))
            {
                Debug.Log("@@@@@@@@@@@@@@@ End Level ");
            }
        }
    }
}
