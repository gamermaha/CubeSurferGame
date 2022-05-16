using UnityEngine;
using DG.Tweening;
using Environment_Setters;

namespace Player_Scripts
{
    public class Magnet : MonoBehaviour
    {
        public GameObject cubeAnimation;
        [SerializeField] private PlayerController player;

        private void Start() => player = FindObjectOfType<PlayerController>();
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Constants.TAG_CUBE))
            {
                GameObject animation = Instantiate(cubeAnimation, other.transform.position, Quaternion.identity);
                player.AddCube(other.gameObject);
                animation.transform.DOMove(player.transform.GetChild(0).position, 0.1f)
                    .SetEase(Ease.InOutFlash).OnComplete(() =>
                    {
                        Destroy(animation);
                    });
                
            }
        }
    }
}
