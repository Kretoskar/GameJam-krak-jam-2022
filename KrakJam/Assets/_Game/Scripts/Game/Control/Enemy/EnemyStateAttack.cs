using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Control.Enemy
{
    public class EnemyStateAttack : MonoBehaviour, IState
    {
        
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