using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : ShootController
{
    [SerializeField] private GameEvent pickRocket;
    public override void PickUp(GunConfig.Type type)
    {
        base.PickUp(type);
        if( type == GunConfig.Type.rocket)
        {
            pickRocket.Raise();
        }
    }
}
