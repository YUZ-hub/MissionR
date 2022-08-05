using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerShoot : ShootController
{
    [SerializeField] private Transform gunTransform;
    public GunType testType;
    private Gun gun;

    private void Start()
    {
        PickUp(testType);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) )
        {
            Trigger();
        }
    }
    override protected void Trigger()
    {
        gun.Shoot();
    }
    override public void Reload()
    {
        gun.Reload();
    }
    public void PickUp(GunType type)
    {
        if( gun != null)
        {
            ObjPoolController.Instance.Release(gun);
        }
        gun = ObjPoolController.Instance.Get(type);
        gun.transform.SetParent(gunTransform);
        gun.transform.localPosition = Vector3.zero;
    }
}
