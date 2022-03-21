using UnityEngine;

namespace Managers
{
    public class EventManager : MonoBehaviour
    {
        private static EventManager eventManager;

        public static EventManager instance
        {
            get
            {
                if (!eventManager)
                {
                    eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;
                    if (!eventManager)
                        Debug.Log("No active Event Manager System.");
                    else
                        eventManager.Init();
                }
                return eventManager;
            }
        }

        private void Init()
        {
            throw new System.NotImplementedException();
        }
    }
}
