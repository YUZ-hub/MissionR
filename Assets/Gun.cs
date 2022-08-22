using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GunConfig config;

    public GunConfig Config { get { return config; } private set { value = config; } }
    public Transform ShootPoint { get { return shootPoint; } private set { value = shootPoint; } }
    public bool IsEmpty() { return bulletNum <= 0;  }
    public bool isCD { get; private set; } = false;
    private int bulletNum;
    private ObjectPool<Bullet> bulletPool;

    private void InitBulletPool()
    {
        bulletPool = new ObjectPool<Bullet>(() =>
        {
            Bullet bullet = Instantiate(config.BulletPrefab);
            bullet.Initial();
            return bullet;
        }, (bullet) => {
            bullet.gameObject.SetActive(true);    
        },(bullet)=>{
            bullet.gameObject.SetActive(false);
        },(bullet)=> {
            Destroy(bullet);
        },true,config.MagazineSize*2, config.MagazineSize * 2);
    }

    public void Shoot()
    {
        if( bulletPool == null)
        {
            InitBulletPool();
        }
        if (isCD)
        {
            return;
        }
        if (IsEmpty())
        {
            if (config.EmptySound.source.isPlaying == false)
            {
                config.EmptySound.source.Play();
            }
            return;
        }
        config.ShootSound.source.Play();
        Bullet bullet = bulletPool.Get();
        bullet.gameObject.transform.position = shootPoint.position;
        bullet.gameObject.transform.rotation = shootPoint.rotation;
        bullet.Rb.AddForce(shootPoint.up * bullet.Config.Force);
        StartCoroutine(AutoRelease(bullet));
        bulletNum -= 1;
        isCD = true;
        StartCoroutine(WaitCd());
    }
    public void Reload()
    {
        config.ReloadSound.source.Play();
        bulletNum = config.MagazineSize;
        isCD = false;
    }
    IEnumerator WaitCd()
    {
        yield return new WaitForSeconds(config.ShootCD);
        isCD = false;
    }
    IEnumerator AutoRelease(Bullet bullet)
    {
        yield return new WaitForSeconds(10f);
        bulletPool.Release(bullet);
    }
}
