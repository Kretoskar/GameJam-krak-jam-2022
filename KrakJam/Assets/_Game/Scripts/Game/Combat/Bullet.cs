using System.Collections;
using System.Collections.Generic;
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

        private void Awake()
        {
            //shitty optimization but fuck it
            playerInput = FindObjectOfType<PlayerInput>();
            rb = GetComponent<Rigidbody2D>();
        }

        public void Shoot()
        {
            Vector2 diff = (playerInput.MousePos - (Vector2) transform.position).normalized;

            float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ - 90);
            
            rb.AddForce(diff * Time.deltaTime * speed, ForceMode2D.Impulse);
        }
    }

}