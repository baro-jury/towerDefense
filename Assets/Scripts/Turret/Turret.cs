using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private float attackRange;
    [SerializeField] private int coinBuy;
    //[SerializeField] private int coinUpgrade;
    //[SerializeField] private int coinSell;

    public float AttackRange { get; set; }
    public int CoinBuy { get; set; }
    public int CoinUpgrade { get; set; }
    public int CoinSell { get; set; }
    public Enemy CurrentEnemyTarget { get; set; }

    private CircleCollider2D circleCollider;

    //private List<Enemy> _enemies;

    protected virtual void Start()
    {
        AttackRange = attackRange;
        CoinBuy = coinBuy;
        CoinUpgrade = coinBuy * 2;
        CoinSell = coinBuy / 2;
        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.radius = AttackRange;

        //_enemies = new List<Enemy>();
    }

    protected virtual void Update()
    {
        //SetCurrentEnemyTarget();
        RotateTowardsTarget();
    }

    //private void SetCurrentEnemyTarget()
    //{
    //    if (_enemies.Count > 0)
    //    {
    //        CurrentEnemyTarget = _enemies[0];
    //    }
    //    else
    //    {
    //        CurrentEnemyTarget = null;
    //    }
    //}

    private void RotateTowardsTarget()
    {
        if (CurrentEnemyTarget == null)
        {
            return;
        }

        Vector3 targetPos = CurrentEnemyTarget.transform.position - transform.position;
        float angle = Vector3.SignedAngle(transform.up, targetPos, transform.forward);
        transform.Rotate(0f, 0f, angle);
    }

    //void OnTriggerEnter2D(Collider2D target)
    //{
    //    if (target.CompareTag("Enemy"))
    //    {
    //        Enemy newEnemy = target.GetComponent<Enemy>();
    //        _enemies.Add(newEnemy);
    //    }
    //}

    //void OnTriggerExit2D(Collider2D target)
    //{
    //    if (target.CompareTag("Enemy"))
    //    {
    //        Enemy enemy = target.GetComponent<Enemy>();
    //        if (_enemies.Contains(enemy))
    //        {
    //            _enemies.Remove(enemy);
    //        }
    //    }
    //}
}