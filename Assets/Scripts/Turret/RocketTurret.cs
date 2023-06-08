using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketTurret : Turret
{
    private List<Enemy> _enemies;

    protected override void Start()
    {
        base.Start();
        _enemies = new List<Enemy>();
    }

    protected override void Update()
    {
        GetCurrentEnemyTarget();
        base.Update();
    }

    private void GetCurrentEnemyTarget()
    {
        if (_enemies.Count > 0)
        {
            CurrentEnemyTarget = _enemies[0];
        }
        else
        {
            CurrentEnemyTarget = null;
        }
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.CompareTag("Enemy"))
        {
            Enemy newEnemy = target.GetComponent<Enemy>();
            _enemies.Add(newEnemy);
        }
    }

    void OnTriggerExit2D(Collider2D target)
    {
        if (target.CompareTag("Enemy"))
        {
            Enemy enemy = target.GetComponent<Enemy>();
            if (_enemies.Contains(enemy))
            {
                _enemies.Remove(enemy);
            }
        }
    }
}
