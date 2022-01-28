using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Game.Combat;
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
        [SerializeField] private Bullet bullet;
        [SerializeField] private float bulletSpawnDistance = 1;
        [SerializeField] private string flySortingLayer;
        [SerializeField] private string startSortingLayer;
        [SerializeField] private AnimationCurve zoomCurve;
        [SerializeField] private AnimationCurve posCurve;
        [SerializeField] private float zoomScale = 1.3f;
        [SerializeField] private float maxJumpDistance = 5;

        private Collider2D coll;

        private SpriteRenderer sr;
        private Animator animator;
        private float currentTime;
        private bool jumped;
        private Grid grid;
        private Vector2 beforeJumpPos;
        private Vector2 targetPos;
        private Vector3 startingScale;

        private void Awake()
        {
            sr = GetComponent<SpriteRenderer>();
            coll = GetComponent<Collider2D>();
            animator = GetComponent<Animator>();

            startingScale = transform.localScale;
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
                    float iterator = (currentTime - chargeTime) / jumpTime;
                    transform.position = Vector2.Lerp(beforeJumpPos, targetPos, posCurve.Evaluate(iterator));
                    transform.localScale = Vector3.Lerp(startingScale, startingScale * zoomScale,
                        zoomCurve.Evaluate(iterator));
                }
            }
            
            currentTime += Time.deltaTime;
        }

        private void Jump()
        {
            sr.sortingLayerID = SortingLayer.NameToID(flySortingLayer);

            beforeJumpPos = transform.position;

            //XDDD
            while (true)
            {
                targetPos = new Vector2(UnityEngine.Random.Range(-xJumpSpace / 2, xJumpSpace / 2), UnityEngine.Random.Range(-yJumpSpace / 2, yJumpSpace / 2));
                
                if(grid.IsPositionWalkable(targetPos) || Vector2.Distance(targetPos, transform.position) > maxJumpDistance)
                    break;
            }

            sr.flipX = targetPos.x > transform.position.x;
            
            jumped = true;
        }

        public void Exit()
        {
            coll.enabled = true;

            sr.sortingLayerID = SortingLayer.NameToID(startSortingLayer);
            
            Shoot();
        }

        private void Shoot()
        {
            var topBullet = Instantiate(bullet, (Vector2)transform.position + Vector2.up * bulletSpawnDistance, Quaternion.identity);
            topBullet.EnemyShoot(DestroyBullet, Vector2.up);
            var botBullet = Instantiate(bullet, (Vector2)transform.position + Vector2.down * bulletSpawnDistance, Quaternion.identity);
            botBullet.EnemyShoot(DestroyBullet, Vector2.down);
            var leftBullet = Instantiate(bullet, (Vector2)transform.position + Vector2.left * bulletSpawnDistance, Quaternion.identity);
            leftBullet.EnemyShoot(DestroyBullet, Vector2.left);
            var righBullet = Instantiate(bullet, (Vector2)transform.position + Vector2.right * bulletSpawnDistance, Quaternion.identity);
            righBullet.EnemyShoot(DestroyBullet, Vector2.right);
        }

        private void OnDrawGizmos()
        {
            if(!drawGizmos) return;
            
            Gizmos.DrawCube(Vector3.zero, new Vector3(xJumpSpace, yJumpSpace, 1));
        }
        
        public void DestroyBullet(Bullet bullet)
        {
            Destroy(bullet.gameObject);
        }

        public bool Finished => true;
        public int Priority { get; set; }
    }

}