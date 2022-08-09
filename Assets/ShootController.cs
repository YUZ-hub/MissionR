using UnityEngine;

public class ShootController : MonoBehaviour
{
    [SerializeField] private Transform gunTransform;
    private Gun gun;
    public bool HaveGun()
    {
        return gun != null;
    }
    public void Trigger()
    {
        if ( gun != null && gun.isCD ==false )
            gun.Shoot();
    }
    public void Reload()
    {
        if (gun != null)
            gun.Reload();
    }
    public void PickUp(GunConfig.Type type)
    {
        if (gun != null )
        {
            if( gun.Config.GunType == type)
            {
                Reload();
                return;
            }
            GunPoolController.Instance.Release(gun);
        }
        gun = GunPoolController.Instance.Get(type);
        gun.transform.SetParent(gunTransform);
        gun.transform.localPosition = Vector3.zero;
        gun.transform.localRotation = Quaternion.identity;
        Reload();
    }
}
