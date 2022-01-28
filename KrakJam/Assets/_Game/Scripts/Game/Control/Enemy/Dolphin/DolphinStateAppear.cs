using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Control.Enemy.Dolphin
{
    public class DolphinStateAppear : MonoBehaviour, IState
    {
        private Collider2D coll;

        private void Awake()
        {
            coll = GetComponent<Collider2D>();
        }
        
        public void Enter(StateMachine sm)
        {
            coll.enabled = false;
        }

        public void Execute()
        {
        }

        public void Exit()
        {
            coll.enabled = true;
        }

        public bool Finished => true;
        public int Priority { get; set; }
    }
}