using System.Collections;
using System.Collections.Generic;
using Game.Control;
using UnityEngine;

namespace Game.Control.Enemy.PC
{
    [RequireComponent(typeof(EnemyPcStateIdle))]
    [RequireComponent(typeof(EnemyPcStateJump))]
    public class EnemyPcStateMachine : StateMachine
    {
        [SerializeField] private float idleTime = 3;
        [SerializeField] private float jumpTime = 2;
        
        private EnemyPcStateIdle idleState;
        private EnemyPcStateJump jumpState;

        private void Awake()
        {
            idleState = GetComponent<EnemyPcStateIdle>();
            jumpState = GetComponent<EnemyPcStateJump>();
        }

        private void Start()
        {
            Behaviour();
        }

        private void Update()
        {
            currentState?.Execute();
        }

        public void Behaviour()
        {
            StartCoroutine(BehaviourCoroutine());
        }

        private IEnumerator BehaviourCoroutine()
        {
            while (true)
            {
                ChangeState(idleState);
                yield return new WaitForSeconds(idleTime);
                ChangeState(jumpState);
                yield return new WaitForSeconds(jumpTime);
            }
        }
    }
}