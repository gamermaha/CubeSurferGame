using System;
using UnityEngine;
using DG.Tweening;
using Managers;

namespace Player_Scripts
{
    public class Magnet : MonoBehaviour
    {
        public GameObject cubeAnimation;
        [SerializeField] private PlayerController player;
        [SerializeField] private GameObject playerCollider;
        private double _cubeSize;

        private void Start()
        {
            player = FindObjectOfType<PlayerController>();
            _cubeSize = MetaData.Instance.scriptableInstance.cubeLength;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Cube"))
            {
                //player.AddCube(other.gameObject);
                GameObject animation = Instantiate(cubeAnimation, other.transform.position, Quaternion.identity);
                animation.transform.DOMove(player.transform.position, 0.2f)
                    .SetEase(Ease.InOutFlash).OnComplete(() =>
                    {
                        Destroy(animation);
                        player.AddCube(other.gameObject);

                    });
                
            }
        }

        public void MagnetPosUpdate(Transform cubePos)
        {
            transform.position += new Vector3(cubePos.position.x, cubePos.position.y, cubePos.position.z - 1f);
        }
    }
}
