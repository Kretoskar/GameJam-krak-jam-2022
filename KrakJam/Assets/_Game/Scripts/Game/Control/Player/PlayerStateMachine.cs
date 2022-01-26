using System.Collections;
using System.Collections.Generic;
using Game.Control;
using Game.Control.Player;
using Game.Input;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Game.Control
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(PlayerStateIdle))]
    [RequireComponent(typeof(PlayerStateMove))]
    public class PlayerStateMachine : StateMachine
    {
        [SerializeField, HideInInspector] private PlayerStateIdle idleState;
        [SerializeField, HideInInspector] private PlayerStateMove moveState;

        private PlayerInput playerInput;

        private void OnValidate()
        {
            idleState = GetComponent<PlayerStateIdle>();
            moveState = GetComponent<PlayerStateMove>();
            playerInput = GetComponent<PlayerInput>();
        }

        private void Update()
        {
            if (playerInput.AxisInput.magnitude > 0.1f)
            {
                ChangeState(moveState);
            }
            else
            {
                ChangeState(idleState);
            }
            
            currentState.Execute();
        }
    }
}