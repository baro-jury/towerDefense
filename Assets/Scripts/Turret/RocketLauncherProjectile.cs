using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncherProjectile : TurretProjectile
{
    protected override void Start()
    {
        _turret = GetComponent<RocketLauncher>();
        base.Start();
    }
}
