using Controllers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameplayUIController : MonoBehaviour
    {
        public GameObject gameStartView;
        public GameObject hUDView;
        public GameObject gameEndView;
        public GameObject gameOverView;
        
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

        public void RestartGame()
        {
            SceneManager.LoadScene("GamePlay");
            gameStartView.SetActive(true);
        }




    }
}
