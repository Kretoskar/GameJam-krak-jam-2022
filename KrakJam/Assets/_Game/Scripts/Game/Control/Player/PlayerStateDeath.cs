using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Control.Player
{
    public class PlayerStateDeath : MonoBehaviour, IState
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