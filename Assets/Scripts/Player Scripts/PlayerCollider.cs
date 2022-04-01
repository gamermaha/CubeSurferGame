using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.Serialization;


namespace Player_Scripts
{
    public class PlayerCollider : MonoBehaviour
    { 
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
            if (other.gameObject.CompareTag("CubeDestroy"))
            {
                Debug.Log(other.gameObject.GetComponent<CubeToDestroy>().obstacleSize);
                player.DestroyCube(other.gameObject, other.gameObject.GetComponent<CubeToDestroy>().obstacleSize);
                transform.localScale -= new Vector3(0f, (float) _cubeSize, 0f);
            }

        }
    }
}
