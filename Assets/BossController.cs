using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] ShootController shoot;
    [SerializeField] BossMove move;

    private void Start()
    {
        StartCoroutine(NormalShoot());
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
}
