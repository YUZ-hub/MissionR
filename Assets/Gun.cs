using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GunConfig config;

    private Queue<GameObject> bulletPool;
    private int bulletNum;
    private bool isCD = false;

    private void OnEnable()
    {
        InitBullet();
    }

    private void InitBullet()
    {
        bulletPool = new Queue<GameObject>();
        for (int i = 0; i < config.MagazineSize; i++)
        {
            GameObject obj = Instantiate(config.BulletPrefab);
            bulletPool.Enqueue(obj);
            obj.SetActive(false);
        }
        bulletNum = config.MagazineSize;
    }
    public void Shoot()
    {
        if (bulletNum <= 0 || isCD)
        {
            if (config.EmptySound.source.isPlaying == false)
            {
                config.EmptySound.source.Play();
            }
            return;
        }
        config.ShootSound.source.Play();
        GameObject bullet = bulletPool.Dequeue();
        bullet.gameObject.transform.position = shootPoint.position;
        bullet.gameObject.transform.rotation = transform.rotation;
        bullet.SetActive(true);
        bulletPool.Enqueue(bullet);
        bulletNum -= 1;
        isCD = true;
        StartCoroutine(WaitCd());
    }
    public void Reload()
    {
        config.ReloadSound.source.Play();
        foreach (GameObject bullet in bulletPool)
        {
            bullet.SetActive(false);
        }
        bulletNum = config.MagazineSize;
    }
    IEnumerator WaitCd()
    {
        yield return new WaitForSeconds(config.ShootCD);
        isCD = false;
    }
}
