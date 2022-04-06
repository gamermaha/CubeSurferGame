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

        private Level Instance;
        private void Awake()
        {
            // if (Instance == null)
            // {
            //     Instance = this;
            //     DontDestroyOnLoad(gameObject);
            // }
            // else
            // {
            //     Destroy(gameObject);
            // }
        }
        private void Start()
        {
            _cubeSize = MetaData.Instance.scriptableInstance.cubeLength;
        }

        public List<Transform> GiveWayPoints()
        {
            return wayPoints;
        }
    }
}
