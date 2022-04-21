using UnityEngine;

namespace Managers
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;
        public const string CUBECOLLECTEDSOUND = "cube_collected";
        public const string DIAMONDCOLLECTEDSOUND = "diamond_collected";
        public const string MAGNETCOLLECTEDSOUND = "magnet_collected";
        public const string DIAMONDMULTIPLIERSOUND = "diamond_multiplier";
        public const string DESTROYCUBESOUND = "cube_destroyed";
        public const string GAMEOVERSOUND = "game_over";
        public const string ENDLEVELSOUND = "end_level";
        public const string GAMESTARTSOUND = "game_is_started";
        public const string GAMECOMPLETEDSOUND = "game_completed";
        
        [SerializeField] private AudioSource cubeCollectSound;
        [SerializeField] private AudioSource diamondCollectSound;
        [SerializeField] private AudioSource magnetCollectSound;
        [SerializeField] private AudioSource diamondMultiplierSound;
        [SerializeField] private AudioSource destroyCubeSound;
        [SerializeField] private AudioSource gameOver;
        [SerializeField] private AudioSource endLevel;
        [SerializeField] private AudioSource gameStart;
        [SerializeField] private AudioSource gameCompleted;
        
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
            switch (action)
            {
                case CUBECOLLECTEDSOUND:
                    cubeCollectSound.Play();
                    break;
                case DIAMONDCOLLECTEDSOUND:
                    diamondCollectSound.Play();
                    break;
                case MAGNETCOLLECTEDSOUND:
                    magnetCollectSound.Play();
                    break;
                case DIAMONDMULTIPLIERSOUND:
                    diamondMultiplierSound.Play();
                    break;
                case DESTROYCUBESOUND:
                    destroyCubeSound.Play();
                    break;
                case GAMEOVERSOUND:
                    gameOver.Play();
                    break;
                case ENDLEVELSOUND:
                    endLevel.Play();
                    break;
                case GAMESTARTSOUND:
                    gameStart.Play();
                    break;
                case GAMECOMPLETEDSOUND:
                    gameCompleted.Play();
                    break;
            }
        }
    }
}

