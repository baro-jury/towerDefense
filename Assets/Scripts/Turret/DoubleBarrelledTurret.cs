using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleBarrelledTurret : Turret
{
    private List<Enemy> _enemies;

    protected override void Start()
    {
        base.Start();
        _enemies = new List<Enemy>();
    }

    protected override void Update()
    {
        SetCurrentEnemyTarget();
        base.Update();
    }

    private void SetCurrentEnemyTarget()
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
