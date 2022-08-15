using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;

[System.Serializable]
public class GunPoolInfo
{
    public Gun gunPrefab;
    public int gunNum;
}

public class GunPoolController : MonoBehaviour
{
    static public GunPoolController Instance;
    [SerializeField] private GunPoolInfo[] gunPoolInfos;

    public Dictionary<GunConfig.Type,ObjectPool<Gun>> gunDictionary;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        gunDictionary = new Dictionary<GunConfig.Type, ObjectPool<Gun>>();
        foreach( GunPoolInfo poolInfo in gunPoolInfos)
        {
            ObjectPool<Gun> pool = new ObjectPool<Gun>(() =>
            {
                return Instantiate(poolInfo.gunPrefab);
            }, (gun) =>
            {
                gun.gameObject.SetActive(true);
            }, (gun) =>
            {
                gun.gameObject.SetActive(false);
            }, (gun) =>
            {
                Destroy(gun.gameObject);
            }, true, poolInfo.gunNum, poolInfo.gunNum);
            gunDictionary.Add(poolInfo.gunPrefab.Config.GunType, pool);
        }
    }
    public void Release(Gun gun)
    {
        gunDictionary[gun.Config.GunType].Release(gun);
    }
    public Gun Get(GunConfig.Type type)
    {
        return gunDictionary[type].Get();
    }
}
