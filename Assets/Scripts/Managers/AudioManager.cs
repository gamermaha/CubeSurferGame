using Environment_Setters;
using UnityEngine;

namespace Managers
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;
        
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
                case Constants.AUDIO_CUBECOLLECTEDSOUND:
                    cubeCollectSound.Play();
                    break;
                case Constants.AUDIO_DIAMONDCOLLECTEDSOUND:
                    diamondCollectSound.Play();
                    break;
                case Constants.AUDIO_MAGNETCOLLECTEDSOUND:
                    magnetCollectSound.Play();
                    break;
                case Constants.AUDIO_DIAMONDMULTIPLIERSOUND:
                    diamondMultiplierSound.Play();
                    break;
                case Constants.AUDIO_DESTROYCUBESOUND:
                    destroyCubeSound.Play();
                    break;
                case Constants.AUDIO_GAMEOVERSOUND:
                    gameOver.Play();
                    break;
                case Constants.AUDIO_ENDLEVELSOUND:
                    endLevel.Play();
                    break;
                case Constants.AUDIO_GAMESTARTSOUND:
                    gameStart.Play();
                    break;
                case Constants.AUDIO_GAMECOMPLETEDSOUND:
                    gameCompleted.Play();
                    break;
            }
        }
    }
}

