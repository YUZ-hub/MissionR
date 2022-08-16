using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShoot : ShootController
{
    public override void PickUp(GunConfig.Type type)
    {
        //switch to a gun prefab
        //set to gun transform to left hand
        //set active false left hand
        base.PickUp(type);
    }
    public override void Trigger()
    {
        base.Trigger();
        if( gun.IsEmpty())
        {
            // set left hand active true
        }
    }
}
