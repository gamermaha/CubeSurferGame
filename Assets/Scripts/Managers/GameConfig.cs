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
        public float xTransSliderMinValue = -10;
        public float xTransSliderMaxValue = 15;
        public float xTransSliderDefValue = 9;
        
        public float yTransSliderMinValue = -10;
        public float yTransSliderMaxValue = 15;
        public float yTransSliderDefValue = 11;
        
        public float zTransSliderMinValue = -50;
        public float zTransSliderMaxValue = 0;
        public float zTransSliderDefValue = -15;
        
        public float xRotSliderMinValue = -45;
        public float xRotSliderMaxValue = 45;
        public float xRotSliderDefValue = 10;
        
        public float yRotSliderMinValue = -45;
        public float yRotSliderMaxValue = 45;
        public float yRotSliderDefValue = -11;

    }
}
