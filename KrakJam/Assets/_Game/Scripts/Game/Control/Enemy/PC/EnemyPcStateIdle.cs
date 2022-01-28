using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Control.Enemy.PC
{
    [RequireComponent(typeof(Animator))]
    public class EnemyPcStateIdle : MonoBehaviour, IState
    {
        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }
        
        public void Enter(StateMachine sm)
        {
        }

        public void Execute()
        {
        }

        public void Exit()
        {
        }

        public bool Finished => true;
        public int Priority { get; set; }
    }

}