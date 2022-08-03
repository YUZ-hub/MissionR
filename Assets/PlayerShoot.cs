using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : ShootController
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private int bulletPoolSize;

    private Queue<GameObject> bulletPool;
    private int bulletNum;

    private void Start()
    {
        bulletPool = new Queue<GameObject>();
        for (int i = 0; i < bulletPoolSize; i++)
        {
            GameObject obj = Instantiate(bulletPrefab);
            bulletPool.Enqueue(obj);
            obj.SetActive(false);
        }
        bulletNum = bulletPoolSize;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }
    override protected void Shoot()
    {
        if (bulletNum <= 0)
        {
            return;
        }
        GameObject bullet = bulletPool.Dequeue();
        bullet.gameObject.transform.position = shootPoint.position;
        bullet.gameObject.transform.rotation = transform.rotation;
        bullet.SetActive(true);
        bulletPool.Enqueue(bullet);
        bulletNum -= 1;
    }
    override public void Reload()
    {
        foreach (GameObject bullet in bulletPool)
        {
            bullet.SetActive(false);
        }
        bulletNum = bulletPoolSize;
    }
}
