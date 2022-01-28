using System.Collections;
using System.Collections.Generic;
using Game.AI.Astar;
using UnityEngine;

namespace Game.Control.Enemy.Dolphin
{
    [RequireComponent(typeof(RigidbodyPathFollower))]
    public class DolphinStateMove : MonoBehaviour, IState
    {
        private RigidbodyPathFollower rbPathFollower;

        private void Awake()
        {
            rbPathFollower = GetComponent<RigidbodyPathFollower>();
        }
        
        public void Enter(StateMachine sm)
        {
            rbPathFollower.ShouldFollow = true;
        }

        public void Execute()
        {
        }

        public void Exit()
        {
            rbPathFollower.ShouldFollow = false;
        }

        public bool Finished => true;
        public int Priority { get; set; }
    }
}