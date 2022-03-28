using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Environment_Setters
{
    public class Level : MonoBehaviour
    {
        public List<GameObject> wayPoints;
        private double _cubeSize;

        private void Start()
        {
            _cubeSize = MetaData.Instance.scriptableInstance.cubeLength;
        }

        public List<GameObject> GiveWayPoints()
        {
            return wayPoints;
        }
    }
}
