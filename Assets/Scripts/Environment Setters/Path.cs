using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Environment_Setters
{
    public class Path : MonoBehaviour
    {
        public GameObject wayPoints;

        public List<GameObject> playerPositions = new List<GameObject>();
        private float _distanceInWeightPoints;
        private float _count;
        private float _pathLength;
        private int _temp = 0;

        
        private float xValue;
        private float yValue;
        private float zValue;
        void Start()
        {
            _pathLength = MetaData.Instance.scriptableInstance.pathLength;

            var transformLocalScale = transform.localScale;
            transformLocalScale.x = _pathLength;
            
            _distanceInWeightPoints = MetaData.Instance.scriptableInstance.distanceInWeightPoints;
            
            _count = _pathLength / _distanceInWeightPoints;
            Debug.Log("The value of count is: " + _count);
            
            xValue = transform.position.x - _pathLength / 2 + _distanceInWeightPoints;
            yValue = transform.position.y + transformLocalScale.y;
            zValue = transform.position.z + transformLocalScale.z/2;
            
            wayPointsSpawner();
        }

        public List<GameObject> wayPointsSpawner()
        {
            while (_temp < _count)
            {
                playerPositions.Add(Instantiate(wayPoints, new Vector3(xValue, yValue, zValue), Quaternion.identity));
                
                Debug.Log("Point " + _temp);
                Debug.Log("The value of x is: "+ xValue);

                xValue += _distanceInWeightPoints;
                _temp++;
            }
            //Debug.Log(playerPositions.Length);
            return playerPositions;
        }

    }
}
