using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Input
{
    public class Cursor : MonoBehaviour
    {
        private PlayerInput playerInput;
        
        private void Start()
        {
            playerInput = FindObjectOfType<PlayerInput>();
            
            UnityEngine.Cursor.visible = false;
        }

        private void Update()
        {
            transform.position = playerInput.MousePos;
        }
    }
}