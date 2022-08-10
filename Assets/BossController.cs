using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] ShootController shoot;
    [SerializeField] BossMove move;
    [SerializeField] Firework fireworkPrefab;

    private void Start()
    {
        shoot.PickUp(GunConfig.Type.pistol);
        FireworkShoot();
    }
    IEnumerator NormalShoot()
    {
        float timeline = 0f;
        while (timeline < 1f)
        {
            move.Aim();
            yield return new WaitForFixedUpdate();
            timeline += Time.fixedDeltaTime;
        }
        yield return new WaitForSeconds(.5f);
        shoot.Trigger();
        //only for test
        StartCoroutine(NormalShoot());
    }
    private void FireworkShoot()
    {
        Firework firework = Instantiate(fireworkPrefab);
        firework.transform.position = shoot.GetGun().ShootPoint.position;
        firework.Shoot();
    }
}