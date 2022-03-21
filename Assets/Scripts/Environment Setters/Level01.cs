using System.Collections.Generic;
using UnityEngine;

namespace Environment_Setters
{
    public class Level01 : MonoBehaviour
    {
        public List<GameObject> wayPoints;

        public List<GameObject> GiveWayPoints()
        {
            return wayPoints;
        }
    }
}
