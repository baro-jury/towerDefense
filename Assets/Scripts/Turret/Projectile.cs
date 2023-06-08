using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public abstract class Projectile : MonoBehaviour
{
    public static Action<Enemy, int> OnEnemyHit;
    public static Action<Enemy> OnEnemyDead;

    [SerializeField] protected float moveSpeed = 10f;
    [SerializeField] protected float minDistanceToDealDamage = 8.1f;

    public TurretProjectile TurretOwner { get; set; }
    public int Damage { get; set; }

    protected Enemy _enemyTarget;

    protected abstract void MoveProjectile();

    protected void RotateProjectile()
    {
        try
        {
            Vector3 enemyPos = _enemyTarget.transform.position - transform.position;
            float angle = Vector3.SignedAngle(transform.up, enemyPos, transform.forward);
            transform.Rotate(0f, 0f, angle);
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    public void SetEnemy(Enemy enemy)
    {
        _enemyTarget = enemy;
    }

}
