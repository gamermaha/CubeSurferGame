using Controllers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Managers
{
    public class GameplayUIController : MonoBehaviour
    {
        public GameObject gameStartView;
        public GameObject hUDView;
        public GameObject gameEndView;
        public GameObject gameOverView;

       

        //private int _playerProgress = 0;

        public void DisableSlider()
        {
            gameStartView.SetActive(false);
            hUDView.SetActive(true);
            InputClass.startMoving = true;
        }
        public void EndGame()
        {
            gameEndView.SetActive(true);
        }
        public void GameOver()
        {
            gameOverView.SetActive(true);
        }


        
        
    }
}
