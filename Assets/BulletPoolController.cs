using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[System.Serializable]
public class BulletPoolInfo
{
    public Bullet bulletPrefab;
    public int bulletNum;
}
public class BulletPoolController : MonoBehaviour
{
    [SerializeField] private BulletPoolInfo[] bulletPoolInfos;

    private Dictionary<BulletConfig.Type, ObjectPool<Bullet>> bulletDictionary;

    public static BulletPoolController Instance { get; private set; }
    private void Awake()
    {
        if( Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        bulletDictionary = new Dictionary<BulletConfig.Type, ObjectPool<Bullet>>();
        foreach( BulletPoolInfo poolInfo in bulletPoolInfos)
        {
            ObjectPool<Bullet> pool = new ObjectPool<Bullet>(() =>
            {
                Bullet bullet =  Instantiate(poolInfo.bulletPrefab);
                bullet.Initial();
                return bullet;
            }, (bullet) =>
            {
                bullet.gameObject.SetActive(true);
            }, (bullet) =>
            {
                bullet.gameObject.SetActive(false);
            }, (bullet) =>
            {
                Destroy(bullet.gameObject);
            }, true, poolInfo.bulletNum, poolInfo.bulletNum);
            bulletDictionary.Add(poolInfo.bulletPrefab.Config.BulletType, pool);
        }
    }
    public void Release(Bullet bullet)
    {
        bulletDictionary[bullet.Config.BulletType].Release(bullet);
    }
    public Bullet Get(BulletConfig.Type type)
    {
        return bulletDictionary[type].Get();
    }
}
