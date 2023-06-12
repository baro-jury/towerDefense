using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectile : MonoBehaviour
{
    [SerializeField] protected int damage = 2;
    [SerializeField] protected float delayPerShot = 2f;
    [SerializeField] protected Transform projectileSpawner;

    public int Damage { get; set; }
    public float DelayPerShot { get; set; }
    public bool CanReload { get; set; }

    protected float _attackTimeInterval;
    protected ProjectileController _projectileController;
    protected Turret _turret;
    protected Projectile _currentProjectileLoaded;

    protected virtual void Start()
    {
        //_turret = GetComponent<Turret>();
        _projectileController = GetComponent<ProjectileController>();

        Damage = damage;
        DelayPerShot = delayPerShot;
        _attackTimeInterval = DelayPerShot;
        LoadProjectile();

    }
    protected virtual void Update()
    {
        CanReload = projectileSpawner.childCount == 0;
        _attackTimeInterval -= Time.deltaTime;
        if (_attackTimeInterval <= 0)
        {
            if (CanReload)
            {
                LoadProjectile();
            }
            if (_turret.CurrentEnemyTarget != null && _currentProjectileLoaded != null)
            {
                _currentProjectileLoaded.transform.parent = null;
                _currentProjectileLoaded.SetEnemy(_turret.CurrentEnemyTarget);
            }
            _attackTimeInterval = DelayPerShot;
        }
    }

    protected virtual void LoadProjectile()
    {
        GameObject projectile = _projectileController.GetProjectileFromMagazine();
        _currentProjectileLoaded = projectile.GetComponent<Projectile>();
        _currentProjectileLoaded.TurretOwner = this;
        _currentProjectileLoaded.Damage = Damage;
        projectile.SetActive(true);
    }

}