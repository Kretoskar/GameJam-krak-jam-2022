using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Game.Input
{
    public class PlayerInput : MonoBehaviour
    {
        private Camera mainCam;
        
        public Vector2 AxisInput => new Vector2(UnityEngine.Input.GetAxis("Horizontal"), UnityEngine.Input.GetAxis("Vertical"));

        public Vector2 MousePos => mainCam.ScreenToWorldPoint(UnityEngine.Input.mousePosition);

        public Action MouseClick;

        private void Awake()
        {
            mainCam = Camera.main;
        }
        
        private void Update()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                MouseClick?.Invoke();
            }
        }
    }
}