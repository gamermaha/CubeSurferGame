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

        private int _audioOnOff;
        
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

        private int GetOnOff()
        {
            _audioOnOff = PlayerPrefs.GetInt("audio", 0) == 1 ? 1 : 0;
            return _audioOnOff;
        }

        public void SetOnOff()
        {
            if (PlayerPrefs.GetInt("audio", 0) == 1)
            {
                PlayerPrefs.SetInt("audio", 0);
                _audioOnOff = 0;
            }
            else if (PlayerPrefs.GetInt("audio", 0) == 0)
            {
                PlayerPrefs.SetInt("audio", 1);
                _audioOnOff = 1;
            }
        }

        public void PlaySounds(string action)
        {
            if (GetOnOff() == 1)
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
}

