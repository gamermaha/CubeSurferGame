using UnityEngine;

namespace Managers
{
    [CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameConfig", order = 1)]
    public class GameConfig : ScriptableObject
    {
        [Header("Player Dynamics")] 
        public float playerSpeed;
        public float playerForce;

        [Header("Environment Dynamics")] 
        public float pathLength;
        public float distanceInWeightPoints;

        [Header("Level 01 Dynamics")] 
        public double cubeLength;

    }
}
