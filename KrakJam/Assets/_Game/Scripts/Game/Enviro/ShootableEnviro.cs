using System.Collections;
using System.Collections.Generic;
using Game.Combat;
using UnityEngine;

namespace Game.Enviro
{
    public class ShootableEnviro : MonoBehaviour
    {
        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var bullet = other.gameObject.GetComponent<Bullet>();
            if (bullet != null)
            {
                animator.SetTrigger("Hit");
            }
        }
    }
}