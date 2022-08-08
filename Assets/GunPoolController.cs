using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;

[System.Serializable]
public class GunPoolInfo
{
    public Gun gunPrefab;
    public int gunNums;
}

public class GunPoolController : MonoBehaviour
{
    static public GunPoolController Instance;
    [SerializeField] GunPoolInfo[] gunPoolInfos;

    public Dictionary<GunType,ObjectPool<Gun>> gunDictionary;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        gunDictionary = new Dictionary<GunType, ObjectPool<Gun>>();
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
            }, true, poolInfo.gunNums, poolInfo.gunNums);
            gunDictionary.Add(poolInfo.gunPrefab.Config.Type, pool);
        }
    }
    public void Release(Gun gun)
    {
        gunDictionary[gun.Config.Type].Release(gun);
    }
    public Gun Get(GunType type)
    {
        return gunDictionary[type].Get();
    }
}
