using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTurretProjectileDetail : TurretProjectile
{
    [SerializeField] private bool isDualMachine;
    [SerializeField] private float spreadRange;

    protected override void Update()
    {
        if (Time.time > _attackTimeInterval)
        {
            if (_turret.CurrentEnemyTarget != null)
            {
                Vector3 dirToTarget = _turret.CurrentEnemyTarget.transform.position - transform.position;
                FireProjectile(dirToTarget);
            }
            _attackTimeInterval = Time.time + delayPerShot;
        }
    }

    protected override void LoadProjectile() { }

    private void FireProjectile(Vector3 direction)
    {
        GameObject instance = _projectileController.GetProjectileFromMagazine();
        instance.transform.position = projectileSpawner.position;

        BulletProjectile projectile = instance.GetComponent<BulletProjectile>();
        //projectile.Direction = direction;
        projectile.TurretOwner.Damage = Damage;
        //AudioManager.instance.PlayerSound(AudioManager.Sound.machineBullet);

        if (isDualMachine)
        {
            float randomSpread = Random.Range(-spreadRange, spreadRange);

            Vector3 spread = new Vector3(0f, 0f, randomSpread);
            Quaternion spreadValue = Quaternion.Euler(spread);
            Vector2 newDirection = spreadValue * direction;
            //projectile.Direction = newDirection;
            //AudioManager.instance.PlayerSound(AudioManager.Sound.machineBullet);
        }
        instance.SetActive(true);
    }
}
