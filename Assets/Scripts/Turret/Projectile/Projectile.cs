using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public abstract class Projectile : MonoBehaviour
{
    public static Action<Enemy, int> OnEnemyDamaged;
    public static Action<Enemy> OnEnemyHit;
    public static Action<Enemy> OnEnemyDead;

    [SerializeField] protected float moveSpeed = 10f;
    [SerializeField] protected float minDistanceToDealDamage = 1f;

    public int Damage { get; set; }
    public TurretProjectile TurretOwner { get; set; }

    protected Enemy _enemyTarget;

    protected virtual void MoveProjectile()
    {
        transform.position = Vector2.MoveTowards(transform.position, _enemyTarget.transform.position, moveSpeed * Time.deltaTime);
        float distanceToTarget = (_enemyTarget.transform.position - transform.position).magnitude;
        if (distanceToTarget < minDistanceToDealDamage)
        {
            _enemyTarget.Health -= Damage;
            if (_enemyTarget.Health > 0)
            {
                OnEnemyHit?.Invoke(_enemyTarget);
            }
            else
            {
                OnEnemyDead?.Invoke(_enemyTarget);
            }

            Destroy(gameObject);
        }
    }

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
