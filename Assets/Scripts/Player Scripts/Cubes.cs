using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Managers;
using TMPro.SpriteAssetUtilities;
using UnityEngine;
using System.Linq;

namespace Player_Scripts
{
    
    public class Cubes : MonoBehaviour
    {
        public static bool OnPath;
        
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Path"))
            {
                OnPath = true;
            }
        }
        private void OnCollisionExit(Collision other)
        {
            if (other.gameObject.CompareTag("Path"))
            {
                OnPath = false;
            }
        }
    }
}
