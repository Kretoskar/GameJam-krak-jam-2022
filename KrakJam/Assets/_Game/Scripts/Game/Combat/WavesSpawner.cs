using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Combat
{
    public class WavesSpawner : MonoBehaviour
    {
        [SerializeField] private List<Wave> waves;
        [SerializeField] [Range(0,100)] private float xSpawnSpace = 10;
        [SerializeField] [Range(0,100)] private float ySpawnSpace = 10;
        [SerializeField] private GameObject winObject;

        [SerializeField] private List<PlayerHealth> playerHp;

        private int enemiesLeft;
        private int currentWave = 0;
        private AI.Astar.Grid grid;
        
        private void Start()
        {
            grid = FindObjectOfType<AI.Astar.Grid>();
            Spawn(waves[0]);
        }

        private void Spawn(Wave wave)
        {
            enemiesLeft = wave.Enemies.Count;

            foreach (var enemy in wave.Enemies)
            {
                Vector2 pos;
                var spawnedEnemy = Instantiate(enemy, SpawnPos(), Quaternion.identity);
                spawnedEnemy.Death += UpdateCounter;
            }
            
            foreach (var playerHealth in playerHp)
            {
                playerHealth.HealAll();
            }
        }

        private void UpdateCounter()
        {
            enemiesLeft--;

            if (enemiesLeft <= 0)
            {
                currentWave++;
                if (waves.Count > currentWave)
                {
                    Spawn(waves[currentWave]);
                }
                else
                {
                    winObject.SetActive(true);
                }
            }
        }

        private Vector2 SpawnPos()
        {
            Vector2 targetPos;
            
            while (true)
            {
                targetPos = new Vector2(UnityEngine.Random.Range(-xSpawnSpace / 2, xSpawnSpace / 2), UnityEngine.Random.Range(-ySpawnSpace / 2, ySpawnSpace / 2));
                
                if(grid.IsPositionWalkable(targetPos))
                    break;
            }

            return targetPos;
        }
    }

    [Serializable]
    public class Wave
    {
        [SerializeField] private List<EnemyHealth> enemies;

        public List<EnemyHealth> Enemies => enemies;
    }
}