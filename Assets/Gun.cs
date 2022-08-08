using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gun : MonoBehaviour
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GunConfig config;

    public GunConfig Config { get { return config; } set { value = config; } }
    public bool isCD { get; private set; } = false;
    private int bulletNum;

    public void Shoot()
    {
        if (bulletNum<=0||isCD)
        {
            if (config.EmptySound.source.isPlaying == false)
            {
                config.EmptySound.source.Play();
            }
            return;
        }
        config.ShootSound.source.Play();
        Bullet bullet = BulletPoolController.Instance.Get(config.BulletType);
        bullet.gameObject.transform.position = shootPoint.position;
        bullet.gameObject.transform.rotation = transform.rotation;
        bullet.Rb.AddForce(transform.right*bullet.Config.Force);
        bulletNum -= 1;
        isCD = true;
        StartCoroutine(WaitCd());
    }
    public void Reload()
    {
        config.ReloadSound.source.Play();
        bulletNum = config.MagazineSize;
    }
    IEnumerator WaitCd()
    {
        yield return new WaitForSeconds(config.ShootCD);
        isCD = false;
    }
}
