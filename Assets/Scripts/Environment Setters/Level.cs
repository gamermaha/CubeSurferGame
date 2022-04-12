using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Environment_Setters
{
    public class Level : MonoBehaviour
    {
        public List<Transform> wayPoints;
        private double _cubeSize;

        [SerializeField]
        public Transform StartPosition;
        public List<Transform> GiveWayPoints()
        {
            return wayPoints;
        }
    }
}
