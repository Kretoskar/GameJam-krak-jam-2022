using System.Collections;
using System.Collections.Generic;
using Game.Combat;
using Game.Control;
using Game.Enviro;
using HutongGames.PlayMaker.Actions;
using UnityEngine;

public class BossStateAttack : MonoBehaviour, IState
{
    [SerializeField] private Bullet bullet;
    [SerializeField] private float bulletSpawnDistance = 1;
    [SerializeField] private float spawnTime = 1;
    [SerializeField] private Transform leftSpawnPos;
    [SerializeField] private Transform rightSpawnPos;
    [SerializeField] private PlayMakerFSM fsm;

    private float t;
    private bool shoot;
    
    public void Enter(StateMachine sm)
    {
        t = 0;
        shoot = false;
    }

    public void Execute()
    {
        t += Time.deltaTime;

        if (t >= spawnTime && !shoot)
        {
            shoot = true;
            Shoot();
        }
    }

    private void Shoot()
    {
        fsm.SendEvent("shake");
        
        //Diagonal
        var topRightBullet = Instantiate(bullet, (Vector2)leftSpawnPos.position + (Vector2.up + Vector2.right) * bulletSpawnDistance, Quaternion.identity);
        topRightBullet.EnemyShoot(DestroyBullet, Vector2.up + Vector2.right);
        var topLeftBullet = Instantiate(bullet, (Vector2)leftSpawnPos.position + (Vector2.up + Vector2.left) * bulletSpawnDistance, Quaternion.identity);
        topLeftBullet.EnemyShoot(DestroyBullet, Vector2.up + Vector2.left);
        var botRightBullet = Instantiate(bullet, (Vector2)leftSpawnPos.position + (Vector2.down + Vector2.right) * bulletSpawnDistance, Quaternion.identity);
        botRightBullet.EnemyShoot(DestroyBullet, Vector2.down + Vector2.right);
        var botLeftBullet = Instantiate(bullet, (Vector2)leftSpawnPos.position + (Vector2.down + Vector2.left) * bulletSpawnDistance, Quaternion.identity);
        botLeftBullet.EnemyShoot(DestroyBullet, Vector2.down + Vector2.left);
        
        //Diagonal
        var rtopRightBullet = Instantiate(bullet, (Vector2)rightSpawnPos.position + (Vector2.up + Vector2.right) * bulletSpawnDistance, Quaternion.identity);
        rtopRightBullet.EnemyShoot(DestroyBullet, Vector2.up + Vector2.right);
        var rtopLeftBullet = Instantiate(bullet, (Vector2)rightSpawnPos.position + (Vector2.up + Vector2.left) * bulletSpawnDistance, Quaternion.identity);
        rtopLeftBullet.EnemyShoot(DestroyBullet, Vector2.up + Vector2.left);
        var rbotRightBullet = Instantiate(bullet, (Vector2)rightSpawnPos.position + (Vector2.down + Vector2.right) * bulletSpawnDistance, Quaternion.identity);
        rbotRightBullet.EnemyShoot(DestroyBullet, Vector2.down + Vector2.right);
        var rbotLeftBullet = Instantiate(bullet, (Vector2)rightSpawnPos.position + (Vector2.down + Vector2.left) * bulletSpawnDistance, Quaternion.identity);
        rbotLeftBullet.EnemyShoot(DestroyBullet, Vector2.down + Vector2.left);
    }

    public void Exit()
    {
    }

    public void DestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    public bool Finished => true;
    public int Priority { get; set; }
}
