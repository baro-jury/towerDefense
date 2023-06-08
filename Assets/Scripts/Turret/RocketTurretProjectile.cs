using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketTurretProjectile : TurretProjectile
{
    
    protected override void Start()
    {
        _turret = GetComponent<RocketTurret>();
        base.Start();
    }

    //protected override void Update()
    //{
        
    //}
}
