using System.Collections;
using System.Collections.Generic;
using Game.AI.Astar;
using Game.Combat;
using Game.Control;
using UnityEngine;

namespace Game.Control.Enemy
{
    [RequireComponent(typeof(EnemyStateAttack))]
    [RequireComponent(typeof(EnemyStateChase))]
    [RequireComponent(typeof(EnemyStateDie))]
    [RequireComponent(typeof(AStarUnit))]
    [RequireComponent(typeof(EnemyHealth))]
    public class EnemyStateMachine : StateMachine
    {
        [SerializeField] private float attackRangeMin = 1;
        [SerializeField] private float attackRangeMax = 1.5f;

        private EnemyStateDie dieState;
        private EnemyStateAttack attackState;
        private EnemyStateChase chaseState;
        private AStarUnit astarUnit;
        private EnemyHealth health;
        
        private void Awake()
        {
            dieState = GetComponent<EnemyStateDie>();
            health = GetComponent<EnemyHealth>();
            attackState = GetComponent<EnemyStateAttack>();
            chaseState = GetComponent<EnemyStateChase>();
            astarUnit = GetComponent<AStarUnit>();
        }

        private void Start()
        {
            ChangeState(attackState);
        }

        private void OnEnable()
        {
            health.Death += Die;
        }

        private void OnDisable()
        {
            health.Death -= Die;
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

        private void Die()
        {
            ChangeState(dieState);
        }
    }
}