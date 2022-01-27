using System.Collections;
using System.Collections.Generic;
using Game.AI.Astar;
using Game.Control;
using UnityEngine;

namespace Game.Control.Enemy
{
    [RequireComponent(typeof(EnemyStateAttack))]
    [RequireComponent(typeof(EnemyStateChase))]
    [RequireComponent(typeof(AStarUnit))]
    public class EnemyStateMachine : StateMachine
    {
        [SerializeField] private float attackRangeMin = 1;
        [SerializeField] private float attackRangeMax = 1.5f;

        private EnemyStateAttack attackState;
        private EnemyStateChase chaseState;
        private AStarUnit astarUnit;

        private void Awake()
        {
            attackState = GetComponent<EnemyStateAttack>();
            chaseState = GetComponent<EnemyStateChase>();
            astarUnit = GetComponent<AStarUnit>();
        }

        private void Start()
        {
            ChangeState(attackState);
        }

        void Update()
        {
            float distance = Vector2.Distance(transform.position, astarUnit.ChooseClosestTarget().position);

            if (ReferenceEquals(currentState, attackState))
            {
                if (distance > attackRangeMax)
                    ChangeState(chaseState);
            }
            else if (ReferenceEquals(currentState, chaseState))
            {
                if (distance < attackRangeMin)
                {
                    ChangeState(attackState);
                }
            }

            currentState?.Execute();
        }
    }
}