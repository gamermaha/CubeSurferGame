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
                Debug.Log("I was null but now my value is: "+ Instance);
                DontDestroyOnLoad(this);
            }
            else 
                Destroy(this);
            
        }
    }
}
