using System.Collections;
using System.Collections.Generic;
using Game.AI.Astar;
using UnityEngine;

namespace Game.AI.Astar
{
    public class RigidbodyPathFollower : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        
        private Rigidbody2D rb;
        private AStarUnit aStarUnit;

        private bool shouldFollow;

        public bool ShouldFollow
        {
            get => shouldFollow;
            set => shouldFollow = value;
        }

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            aStarUnit = GetComponent<AStarUnit>();
        }

        private void Update()
        {
            if(shouldFollow)
                TryMove();
        }

        private void TryMove()
        {
            Vector2 moveDir = Vector2.zero;
            
            if (aStarUnit.Path == null || aStarUnit.Path.Length == 0) return;

            if (aStarUnit.Path.Length > 1)
            {
                Move(rb.position +
                     ((Vector2) aStarUnit.Path[1] - rb.position) * Time.fixedDeltaTime * moveSpeed);
            }
            else
            {
                Move(rb.position +
                     ((Vector2) aStarUnit.Path[0] - rb.position) * Time.fixedDeltaTime * moveSpeed);
            }
        }

        private void Move(Vector2 pos)
        {
            rb.MovePosition(pos);
        }
    }
}