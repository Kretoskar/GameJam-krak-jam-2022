using System.Collections;
using System.Collections.Generic;
using Game.Combat;
using Game.Control;
using Game.Input;
using UnityEngine;

namespace Game.Control.Player
{
    public class PlayerAsyncStateShoot : MonoBehaviour, IAsynchronousState
    {
        [SerializeField] private Bullet bullet;
        [SerializeField] [Range(0,10)] private float spawnDistance = 0.5f;
        
        private PlayerInput playerInput;

        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
        }

        private void OnEnable()
        {
            playerInput.MouseClick += Shoot;
        }

        private void OnDisable()
        {
            playerInput.MouseClick -= Shoot;
        }
        
        public void Enter()
        {
            
        }

        public void Execute()
        {

        }

        public void Disturb()
        {

        }

        public void Exit()
        {

        }

        private void Shoot()
        {
            var spawnedBullet = 
                Instantiate(bullet, 
                    (Vector2) transform.position + (playerInput.MousePos - (Vector2) transform.position).normalized * spawnDistance,
                Quaternion.identity);
            
            spawnedBullet.GetComponent<Bullet>().Shoot();
        }
    }
}