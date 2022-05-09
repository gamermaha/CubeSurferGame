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

        private void Start() => player = FindObjectOfType<PlayerController>();
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Cube"))
            {
                GameObject animation = Instantiate(cubeAnimation, other.transform.position, Quaternion.identity);
                animation.transform.DOMove(player.transform.position, 0.2f)
                    .SetEase(Ease.InOutFlash).OnComplete(() =>
                    {
                        Destroy(animation);
                        player.AddCube(other.gameObject);
                    });
                
            }
        }
    }
}
