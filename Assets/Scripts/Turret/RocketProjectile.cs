using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketProjectile : Projectile
{
    void Update()
    {
        if (_enemyTarget != null)
        {
            MoveProjectile();
            RotateProjectile();
        }
    }

    protected override void MoveProjectile()
    {
        try
        {
            transform.position = Vector2.MoveTowards(transform.position, _enemyTarget.transform.position, moveSpeed * Time.deltaTime);
            float distanceToTarget = (_enemyTarget.transform.position - transform.position).magnitude;
            if (distanceToTarget < minDistanceToDealDamage)
            {
                OnEnemyHit?.Invoke(_enemyTarget, Damage);
                if (_enemyTarget.Health <= 0)
                {
                    OnEnemyDead?.Invoke(_enemyTarget);
                }
                TurretOwner.CanReload = true;
                Destroy(gameObject);
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
}
