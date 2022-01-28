using System.Collections;
using System.Collections.Generic;
using Game.Combat;
using UnityEngine;

namespace Game.Control.Enemy.Dolphin
{
    [RequireComponent(typeof(DolphinStateAppear))]
    [RequireComponent(typeof(DolphinStateMove))]
    [RequireComponent(typeof(DolphinStateAttack))]
    public class DoplhinStateMachine : StateMachine
    {
        [SerializeField] private float appearTime = 1;
        [SerializeField] private float moveTime = 5;
        [SerializeField] private float attackTime = 3;

        private EnemyHealth health;
        
        private DolphinStateAppear appearState;
        private DolphinStateMove moveState;
        private DolphinStateAttack attackState;

        private void Awake()
        {
            health = GetComponent<EnemyHealth>();
            appearState = GetComponent<DolphinStateAppear>();
            moveState = GetComponent<DolphinStateMove>();
            attackState = GetComponent<DolphinStateAttack>();
        }

        private void OnEnable()
        {
            health.Death += Die;
        }

        private void OnDisable()
        {
            health.Death -= Die;
        }

        private void Start()
        {
            StartCoroutine(SmCoroutine());
        }

        private void Die()
        {
            StopAllCoroutines();
            Destroy(gameObject);
        }
        
        private IEnumerator SmCoroutine()
        {
            while (true)
            {
                ChangeState(appearState);
                yield return  new WaitForSeconds(appearTime);
                ChangeState(moveState);
                yield return new WaitForSeconds(moveTime);
                ChangeState(attackState);
                yield return new WaitForSeconds(attackTime);
            }
        }
    }
}