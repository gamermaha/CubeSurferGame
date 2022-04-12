using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


namespace Controllers
{
    public class SaveController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                string activeScene = SceneManager.GetActiveScene().name;
                PlayerPrefs.SetString("LevelSaved", activeScene);
                Debug.Log(activeScene);

                gameObject.SetActive(false);
            }
        }
    }
}
