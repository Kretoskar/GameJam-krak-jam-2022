using System.Collections;
using System.Collections.Generic;
using Game.Combat;
using UnityEngine;

namespace Game.Control.Enemy
{
    public class EnemyStateDie : MonoBehaviour, IState
    {
        public void Enter(StateMachine sm)
        {
            Destroy(gameObject);
        }

        public void Execute()
        {
        }

        public void Exit()
        {
        }

        public bool Finished => false;
        public int Priority { get; set; }
    }
}