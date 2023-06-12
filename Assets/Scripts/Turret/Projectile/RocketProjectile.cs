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
        #region Separately
        //try
        //{
        //    transform.position = Vector2.MoveTowards(transform.position, _enemyTarget.transform.position, moveSpeed * Time.deltaTime);
        //    float distanceToTarget = (_enemyTarget.transform.position - transform.position).magnitude;
        //    if (distanceToTarget < minDistanceToDealDamage)
        //    {
        //        _enemyTarget.Health -= Damage;
        //        if (_enemyTarget.Health > 0)
        //        {
        //            OnEnemyHit?.Invoke(_enemyTarget);
        //        }
        //        else
        //        {
        //            OnEnemyDead?.Invoke(_enemyTarget);
        //        }
        //        Destroy(gameObject);
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Debug.Log(ex.Message);
        //} 
        #endregion

        base.MoveProjectile();
    }
}
