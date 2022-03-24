using UnityEngine;

namespace Managers
{
    public class EventManager : MonoBehaviour
    {
        private static EventManager _eventManager;

        public static EventManager instance
        {
            get
            {
                if (!_eventManager)
                {
                    _eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;
                    if (!_eventManager)
                        Debug.Log("No active Event Manager System.");
                    else
                        _eventManager.Init();
                }
                return _eventManager;
            }
        }

        private void Init()
        {
            throw new System.NotImplementedException();
        }
    }
}
