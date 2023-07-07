using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketProjectile : Projectile
{
    protected override void Update()
    {
        if (_enemyTarget != null)
        {
            MoveProjectile();
        }
        CheckProjectilePosition();
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

        transform.Translate(transform.up * moveSpeed * Time.deltaTime, Space.World);
    }

    void CheckProjectilePosition()
    {
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

        if (viewportPosition.x < 0 || viewportPosition.y < 0 || viewportPosition.x > 1 || viewportPosition.y > 1)
        {
            Destroy(gameObject); ;
        }
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.CompareTag("Enemy"))
        {
            Enemy enemy = target.GetComponent<Enemy>();
            enemy.Health -= TurretOwner.Damage;
            if (enemy.Health > 0)
            {
                OnEnemyHit?.Invoke(enemy);
            }
            else
            {
                OnEnemyDead?.Invoke(enemy);
            }
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        //Destroy(gameObject);
    }
}
