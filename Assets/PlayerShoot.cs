using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerShoot : ShootController
{
    [SerializeField] private Transform gunTransform;
    [SerializeField] private Gun gun;

    private void Start()
    {
        PickUp(gun);
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
    public void PickUp(Gun _gun)
    {
        Gun obj = Instantiate(_gun);       
        obj.transform.SetParent(gunTransform);
        obj.transform.localPosition = Vector3.zero;
        gun = obj;
    }
}
