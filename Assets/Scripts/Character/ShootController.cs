using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class GunGenerateInfo
{
    public GameObject gunPrefab;
    public GunConfig.Type type;
}
public class ShootController : MonoBehaviour
{
    [SerializeField] protected Transform gunTransform;
    [SerializeField] List<GunGenerateInfo> infos = new List<GunGenerateInfo>();

    private Dictionary<GunConfig.Type, Gun> gunDictionary = new Dictionary<GunConfig.Type, Gun>();
    public Gun gun { get; protected set; }

    private void Start()
    {
        foreach(GunGenerateInfo info in infos)
        {
            if(Instantiate(info.gunPrefab).TryGetComponent(out Gun g))
            {
                g.gameObject.transform.SetParent(gunTransform);
                g.gameObject.transform.localPosition = Vector3.zero;
                g.gameObject.transform.rotation = gunTransform.rotation;
                gunDictionary.Add(info.type, g);
                g.gameObject.SetActive(false);
            }
        }
    }
    public virtual void Trigger()
    {
        if (gun == null)
            return;
        gun.Shoot();
    }
    public virtual void PickUp(GunConfig.Type type)
    {
        if (gun != null )
        {
            if( gun.Config.GunType == type)
            {
                gun.Reload();
            }
            else
            {
                gun.gameObject.SetActive(false);
            }
        }
        if( gunDictionary.TryGetValue(type,out Gun g))
        {
            gun = g;
            gun.gameObject.SetActive(true);
            gun.Reload();
        }    
    }
}
