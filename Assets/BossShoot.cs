using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShoot : ShootController
{
    [SerializeField] private GameObject leftHand;
    public override void Trigger()
    {
        base.Trigger();
        if (gun.IsEmpty())
        {
            GunPoolController.Instance.Release(gun);
            leftHand.SetActive(true);
        }
    }

    public override void PickUp(GunConfig.Type type)
    {
        base.PickUp(type);
        leftHand.SetActive(false);
    }
}
