﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{ 
    public class GameStartView : MonoBehaviour
    {
        public Slider handSlider;

        private void Start()
        {
            handSlider.value = 0;
        }

        public IEnumerator HandSlider()
        {
            while (handSlider.value < 2f)
            {
                yield return new WaitForSeconds(0.05f);
                handSlider.value += 0.1f;
                if (handSlider.value >= 1)
                    handSlider.value = 0;
            }
        }
        // public void SetActiveTrue()
        // {
        //     gameObject.SetActive(true);
        //     
        // }
        // public void SetActiveFalse()
        // {
        //     
        //     gameObject.SetActive(false);
        // }
        
    }
}