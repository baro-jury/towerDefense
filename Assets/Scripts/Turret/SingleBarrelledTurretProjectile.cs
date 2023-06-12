using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleBarrelledTurretProjectile : TurretProjectile
{
    protected override void Start()
    {
        _turret = GetComponent<SingleBarrelledTurret>();
        base.Start();
    }
}
