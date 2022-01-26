using System.Collections;
using System.Collections.Generic;
using Game.Input;
using UnityEngine;

namespace Game.Control.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerStateMove : MonoBehaviour, IState
    {
        [SerializeField] [Range(-100, 100)] private float moveSpeed = 10;
        
        private PlayerInput playerInput;
        private Rigidbody2D rb;

        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
            rb = GetComponent<Rigidbody2D>();
        }
        
        public void Enter(StateMachine sm)
        {
            
        }

        public void Execute()
        {
            rb.MovePosition(rb.position + playerInput.AxisInput.normalized * Time.deltaTime * moveSpeed);
        }

        public void Exit()
        {
            
        }

        public bool Finished => true;
        public int Priority { get; set; }
    }

}