using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Game.Control.Player;
using Game.Input;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Combat
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] [Range(0.01f, 1000)] private float speed = 100;

        private PlayerInput playerInput;
        private Rigidbody2D rb;
        private bool deflected;

        private Action<Bullet> destroyAction;

        private Vector2 shootDir;

        private void Awake()
        {
            //shitty optimization but fuck it
            playerInput = FindObjectOfType<PlayerInput>();
            rb = GetComponent<Rigidbody2D>();
        }

        public void Shoot(Action<Bullet> destroyAction, Transform shooter)
        {
            deflected = false;
            
            this.destroyAction = destroyAction;
            
            Vector2 diff = (playerInput.MousePos - (Vector2) shooter.position).normalized;

            float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ - 90);

            shootDir = diff * Time.fixedDeltaTime * speed;
            
            rb.AddForce(diff.normalized * Time.fixedDeltaTime * speed, ForceMode2D.Impulse);
        }

        public void EnemyShoot(Action<Bullet> destroyAction, Vector2 direction)
        {
            this.destroyAction = destroyAction;

            Vector2 diff = direction;

            float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ - 90);

            shootDir = diff * Time.fixedDeltaTime * speed;
            
            rb.AddForce(diff.normalized * Time.fixedDeltaTime * speed, ForceMode2D.Impulse);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            Health health = col.gameObject.GetComponent<Health>();

            if (health == null && col.transform.parent != null)
            {
                health = col.transform.parent.GetComponent<Health>();
            }


            if (health != null)
            {
                health.GetHit();
                destroyAction(this);
                return;
            }

            if (deflected)
            {
                destroyAction(this);
            }
            else
            {
                Deflect(col);
                deflected = true;
            }
        }

        private void Deflect(Collision2D coll)
        {
            Vector2 reflectedPosition = Vector3.Reflect(transform.up, coll.contacts[0].normal);
            rb.velocity = reflectedPosition.normalized * Time.fixedDeltaTime * speed;
            
            float rotZ = Mathf.Atan2(-reflectedPosition.y, -reflectedPosition.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + 90);
        }
    }

}