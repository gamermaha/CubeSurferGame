using UnityEngine;

namespace Managers
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource cubeCollectSound;
        [SerializeField] private AudioSource diamondCollectSound;
        [SerializeField] private AudioSource magnetCollectSound;
        [SerializeField] private AudioSource diamondMultiplierSound;
        
        [SerializeField] private AudioSource destroyCubeSound;
        [SerializeField] private AudioSource gameOver;
        [SerializeField] private AudioSource endLevel;
        [SerializeField] private AudioSource gameStart;
        [SerializeField] private AudioSource gameCompleted;


        public static AudioManager Instance;

        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else 
                Destroy(this);
        }

        public void PlaySounds(string action)
        {
            if (action == "cube collected")
                cubeCollectSound.Play();
            
            else if (action == "diamond collected")
                diamondCollectSound.Play();
            
            else if (action == "magnet collected") 
                magnetCollectSound.Play();
            
            else if (action == "diamond multiplier")
                diamondMultiplierSound.Play();
            
            else if (action == "cube destroyed")
                destroyCubeSound.Play();
            
            else if (action == "game over")
                gameOver.Play();
            
            else if (action == "end level")
                endLevel.Play();
            
            else if (action == "game is started")
                gameStart.Play();
            
            else if (action == "game completed")
                gameCompleted.Play();
        }
        
    }
}

