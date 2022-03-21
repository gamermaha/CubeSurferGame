using UnityEngine;

namespace Managers
{
    public class AudioManager : MonoBehaviour
    {
        public AudioSource exampleSounds;


        public static AudioManager instance;

        public void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this);
            }
            else 
                Destroy(this);
        }
    }
}
