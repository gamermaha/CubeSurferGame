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
        public int noOfLevels;
        public double cubeLength;
        public float destroyMagnetTime;
        public float diamondTimer;
        
        [Header("Camera Config")]
        public float xTransSliderMinValue = -20;
        public float xTransSliderMaxValue = 20;
        public float xTransSliderDefValue = 10;
        
        public float yTransSliderMinValue = 0;
        public float yTransSliderMaxValue = 40;
        public float yTransSliderDefValue = 20;
        
        public float zTransSliderMinValue = -75;
        public float zTransSliderMaxValue = -25;
        public float zTransSliderDefValue = -50;
        
        public float xRotSliderMinValue = -45;
        public float xRotSliderMaxValue = 45;
        public float xRotSliderDefValue = 0;
        
        public float yRotSliderMinValue = -45;
        public float yRotSliderMaxValue = 45;
        public float yRotSliderDefValue = 0;

    }
}
