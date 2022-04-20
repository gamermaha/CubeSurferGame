using UnityEngine;

namespace Managers
{
    public class AudioManager : MonoBehaviour
    {
        public static string CUBECOLLECTEDSOUND = "cube_collected";
        public static string DIAMONDCOLLECTEDSOUND = "diamond_collected";
        public static string MAGNETCOLLECTEDSOUND = "magnet_collected";
        public static string DIAMONDMULTIPLIERSOUND = "diamond_multiplier";
        public static string DESTROYCUBESOUND = "cube_destroyed";
        public static string GAMEOVERSOUND = "game_over";
        public static string ENDLEVELSOUND = "end_level";
        public static string GAMESTARTSOUND = "game_is_started";
        public static string GAMECOMPLETEDSOUND = "game_completed";
        
        
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
            if (action == CUBECOLLECTEDSOUND)
                cubeCollectSound.Play();
            
            else if (action == DIAMONDCOLLECTEDSOUND)
                diamondCollectSound.Play();
            
            else if (action == MAGNETCOLLECTEDSOUND) 
                magnetCollectSound.Play();
            
            else if (action == DIAMONDMULTIPLIERSOUND)
                diamondMultiplierSound.Play();
            
            else if (action == DESTROYCUBESOUND)
                destroyCubeSound.Play();
            
            else if (action == GAMEOVERSOUND)
                gameOver.Play();
            
            else if (action == ENDLEVELSOUND)
                endLevel.Play();
            
            else if (action == GAMESTARTSOUND)
                gameStart.Play();
            
            else if (action == GAMECOMPLETEDSOUND)
                gameCompleted.Play();
        }
        
    }
}

