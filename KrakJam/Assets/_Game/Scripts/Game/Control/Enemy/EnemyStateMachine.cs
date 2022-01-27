using System.Collections;
using System.Collections.Generic;
using Game.AI.Astar;
using Game.Control;
using UnityEngine;

namespace Game.Control.Enemy
{
    [RequireComponent(typeof(EnemyStateIdle))]
    [RequireComponent(typeof(EnemyStateChase))]
    [RequireComponent(typeof(AStarUnit))]
    public class EnemyStateMachine : StateMachine
    {
        [SerializeField] private float attackRange = 1;
        
        private EnemyStateIdle idleState;
        private EnemyStateChase chaseState;
        private AStarUnit astarUnit;

        private void Awake()
        {
            idleState = GetComponent<EnemyStateIdle>();
            chaseState = GetComponent<EnemyStateChase>();
            astarUnit = GetComponent<AStarUnit>();
        }

        void Update()
        {
            float distance = Vector2.Distance(transform.position, astarUnit.ChooseClosestTarget().position);
            
            if(distance > attackRange)
                ChangeState(chaseState);
            else
                ChangeState(idleState);
            
            currentState?.Execute();
        }
    }

}