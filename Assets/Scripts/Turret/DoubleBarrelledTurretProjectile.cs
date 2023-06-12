using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleBarrelledTurretProjectile : TurretProjectile
{
    protected override void Start()
    {
        _turret = GetComponent<DoubleBarrelledTurret>();
        base.Start();
    }
}
