using UnityEngine;

namespace Managers
{
    [CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameConfig", order = 1)]
    public class GameConfig : ScriptableObject
    {
        [Header("Player Dynamics")] 
        public float playerSpeed;

        [Header("Environment Dynamics")] 
        public float pathLength;
        public float distanceInWeightPoints;

        [Header("Level Dynamics")] 
        public int noOflevels;
        public double cubeLength;
        public float destroyMagnetTime;
        public float diamondTimer;

    }
}
