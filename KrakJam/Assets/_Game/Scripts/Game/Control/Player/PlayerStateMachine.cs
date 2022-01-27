using System.Collections;
using System.Collections.Generic;
using Game.Combat;
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
    [RequireComponent(typeof(PlayerStateDeath))]
    [RequireComponent(typeof(PlayerHealth))]
    public class PlayerStateMachine : StateMachine
    {
        private PlayerHealth health;
        private PlayerStateIdle idleState;
        private PlayerStateMove moveState;
        private PlayerStateDeath deathState;

        private PlayerInput playerInput;

        private void Awake()
        {
            health = GetComponent<PlayerHealth>();
            idleState = GetComponent<PlayerStateIdle>();
            moveState = GetComponent<PlayerStateMove>();
            deathState = GetComponent<PlayerStateDeath>();
            playerInput = GetComponent<PlayerInput>();
        }

        private void OnEnable()
        {
            health.Death += Die;
        }

        private void OnDisable()
        {
            health.Death -= Die;
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

        private void Die()
        {
            ChangeState(deathState);
        }
    }
}