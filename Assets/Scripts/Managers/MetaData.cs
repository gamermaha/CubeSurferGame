using UnityEngine;

namespace Managers
{
    public class MetaData : MonoBehaviour
    {
        public static MetaData Instance;
        public GameConfig scriptableInstance;

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
    }
}
