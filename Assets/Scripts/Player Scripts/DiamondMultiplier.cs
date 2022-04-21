using UnityEngine;
using DG.Tweening;
using Managers;

namespace Player_Scripts
{
    public class DiamondMultiplier : MonoBehaviour
    {
        //public GameObject cubeAnimation;
        [SerializeField] private PlayerController player;

        private void Start()
        {
            player = FindObjectOfType<PlayerController>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Diamond"))
            {
                
                
                
            }
        }
    }
}
