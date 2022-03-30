
using System.Collections.Generic;
using Controllers;
using Managers;
using UnityEngine;

namespace Player_Scripts
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private GameObject cubeCollector;
        [SerializeField] private PlayerCollider playerCollider;

        

        private InputClass inputManager;
        private float _mySpeed;
        private float _moveForce;

        
        
        private int _wayPtIncrement;
        
        private float _xValue;
        private float _yValue;
        private float _zValue;
        private double _cubeSize;
        
        private Vector3 _prevMousePos;
        private Vector3 _prevPlayerPos;

        private List<Transform> _playerPositions;
        private List<GameObject> _cubes = new List<GameObject>();

        void Awake()
        {
            inputManager = GetComponent<InputClass>();
        }
        private void Start()
        {
            if (MetaData.Instance == null)
            {
                _mySpeed = 0;
                _moveForce = 0;
            }
            else
            {
                _mySpeed = MetaData.Instance.scriptableInstance.playerSpeed;
                _moveForce = MetaData.Instance.scriptableInstance.playerForce;
            }
            _cubeSize = MetaData.Instance.scriptableInstance.cubeLength;
        }
        private void Update()
        {
            if (PlayerCollider.AddCube)
                AddCube();
            
            if (PlayerCollider.DestroyCube)
                DestroyCube();


        }

        public void AddCube()
        {
            
        }

        public void DestroyCube()
        {
            
        }
        
    }
}
