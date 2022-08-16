using System.Collections;
using UnityEngine;


public class Gun : MonoBehaviour
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GunConfig config;

    public GunConfig Config { get { return config; } private set { value = config; } }
    public Transform ShootPoint { get { return shootPoint; } private set { value = shootPoint; } }
    public bool IsEmpty() { return bulletNum <= 0;  }
    public bool isCD { get; private set; } = false;
    private int bulletNum;

    public void Shoot()
    {
        if (IsEmpty()||isCD)
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
        bullet.gameObject.transform.rotation = shootPoint.rotation;
        bullet.Rb.AddForce(shootPoint.up*bullet.Config.Force);
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
