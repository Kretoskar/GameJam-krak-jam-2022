using System.Collections;
using System.Collections.Generic;
using Game.Combat;
using Game.Control;
using Game.Input;
using UnityEngine;
using UnityEngine.Pool;

namespace Game.Control.Player
{
    public class PlayerAsyncStateShoot : MonoBehaviour, IAsynchronousState
    {
        [SerializeField] private Bullet bullet;
        [SerializeField] [Range(0,10)] private float spawnDistance = 0.5f;
        
        private PlayerInput playerInput;

        private ObjectPool<Bullet> bulletPool;

        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
        }

        private void Start()
        {
            bulletPool = new ObjectPool<Bullet>(
                InstantiatePrefab,
                GetFromPool,
                AddToPool,
                bullet => { Destroy(bullet.gameObject);},
                true,
                100,
                1000
            );
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
            bulletPool.Get();
        }

        private Bullet InstantiatePrefab()
        {
            var spawnedBullet = 
                Instantiate(bullet, 
                    (Vector2) transform.position + (playerInput.MousePos - (Vector2) transform.position).normalized * spawnDistance,
                    Quaternion.identity);
            
            spawnedBullet.GetComponent<Bullet>().Shoot(DestroyBullet);

            return spawnedBullet;
        }

        private void GetFromPool(Bullet bullet)
        {
            bullet.gameObject.SetActive(true);
            bullet.transform.position = (Vector2) transform.position +
                                        (playerInput.MousePos - (Vector2) transform.position).normalized *
                                        spawnDistance;
            bullet.Shoot(DestroyBullet);
        }


        private void AddToPool(Bullet bullet)
        {
            bullet.gameObject.SetActive(false);
        }

        public void DestroyBullet(Bullet bullet)
        {
            bulletPool.Release(bullet);
        }
    }
}