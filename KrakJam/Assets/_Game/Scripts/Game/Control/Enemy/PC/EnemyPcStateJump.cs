using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grid = Game.AI.Astar.Grid;

namespace Game.Control.Enemy.PC
{
    [RequireComponent(typeof(Animator))]
    public class EnemyPcStateJump : MonoBehaviour, IState
    {
        [SerializeField] private float chargeTime = 0.95f;
        [SerializeField] private float jumpTime = 0.95f;
        [SerializeField] [Range(0,100)] private float xJumpSpace = 10;
        [SerializeField] [Range(0,100)] private float yJumpSpace = 10;
        [SerializeField] private bool drawGizmos = false;

        private Collider2D coll;

        private SpriteRenderer sr;
        private Animator animator;
        private float currentTime;
        private bool jumped;
        private Grid grid;
        private Vector2 beforeJumpPos;
        private Vector2 targetPos;

        private void Awake()
        {
            sr = GetComponent<SpriteRenderer>();
            coll = GetComponent<Collider2D>();
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            grid = FindObjectOfType<Grid>();
        }
        
        public void Enter(StateMachine sm)
        {
            animator.SetTrigger("Jump");
            currentTime = 0;
            jumped = false;
            beforeJumpPos = Vector2.zero;
            targetPos = Vector2.zero;

            coll.enabled = false;
        }

        public void Execute()
        {
            if (currentTime > chargeTime)
            {
                if (!jumped)
                {
                    Jump();
                    jumped = true;
                }
                else
                {
                    transform.position = Vector2.Lerp(beforeJumpPos, targetPos, ((currentTime - chargeTime) / jumpTime));
                }
            }
            
            currentTime += Time.deltaTime;
        }

        private void Jump()
        {
            beforeJumpPos = transform.position;

            while (true)
            {
                targetPos = new Vector2(UnityEngine.Random.Range(-xJumpSpace, xJumpSpace), UnityEngine.Random.Range(-yJumpSpace, yJumpSpace));
                
                if(grid.IsPositionWalkable(targetPos))
                    break;
            }

            sr.flipX = targetPos.x > transform.position.x;
            
            jumped = true;
        }

        public void Exit()
        {
            coll.enabled = true;
            
            Shoot();
        }

        private void Shoot()
        {
            
        }

        private void OnDrawGizmos()
        {
            if(!drawGizmos) return;
            
            Gizmos.DrawCube(Vector3.zero, new Vector3(xJumpSpace, yJumpSpace, 1));
        }

        public bool Finished => true;
        public int Priority { get; set; }
    }

}