using System.Collections;
using System.Collections.Generic;
using Game.AI.Astar;
using UnityEngine;

namespace Game.Control.Enemy
{
    [RequireComponent(typeof(RigidbodyPathFollower))]
    public class EnemyStateChase : MonoBehaviour, IState
    {
        private RigidbodyPathFollower seeker;

        private void Awake()
        {
            seeker = GetComponent<RigidbodyPathFollower>();
        }
        
        public void Enter(StateMachine sm)
        {
            seeker.ShouldFollow = true;
        }

        public void Execute()
        {
        }

        public void Exit()
        {
            seeker.ShouldFollow = false;
        }

        public bool Finished => true;
        public int Priority { get; set; }
    }
}