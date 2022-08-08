using UnityEngine;

public class ShootController : MonoBehaviour
{
    [SerializeField] private Transform gunTransform;
    public GunConfig.Type testType;
    private Gun gun;

    private void Start()
    {
        PickUp(testType);
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
        if (gun != null)
        {
            GunPoolController.Instance.Release(gun);
        }
        gun = GunPoolController.Instance.Get(type);
        gun.transform.SetParent(gunTransform);
        gun.transform.localPosition = Vector3.zero;
        Reload();
    }
}
