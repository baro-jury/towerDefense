using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectile : MonoBehaviour
{
    [SerializeField] protected Transform projectileSpawner;
    [SerializeField] protected int damage = 2;
    [SerializeField] protected float attackTimeInterval = 2f;

    public int Damage { get; set; }
    public float DelayPerShot { get; set; }
    public bool CanReload { get; set; }

    protected float _nextAttackTime;
    protected ProjectileController _projectilePool;
    protected Turret _turret;
    protected Projectile _currentProjectileLoaded;

    protected virtual void Start()
    {
        //_turret = GetComponent<Turret>();
        _projectilePool = GetComponent<ProjectileController>();

        Damage = damage;
        DelayPerShot = attackTimeInterval;
        _nextAttackTime = DelayPerShot;
        LoadProjectile();
        CanReload = false;

    }
    protected virtual void Update()
    {
        if (CanReload)
        {
            LoadProjectile();
        }

        _nextAttackTime -= Time.deltaTime;
        if (_nextAttackTime <= 0)
        {
            if (_turret.CurrentEnemyTarget != null && _currentProjectileLoaded != null)
            {
                _currentProjectileLoaded.SetEnemy(_turret.CurrentEnemyTarget);
            }
            _nextAttackTime = DelayPerShot;
        }
    }

    protected virtual void LoadProjectile()
    {
        GameObject newInstance = _projectilePool.GetProjectileFromPool();

        _currentProjectileLoaded = newInstance.GetComponent<Projectile>();
        _currentProjectileLoaded.TurretOwner = this;
        _currentProjectileLoaded.Damage = Damage;
        newInstance.SetActive(true);

        CanReload = false;
    }

}