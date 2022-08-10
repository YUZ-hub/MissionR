using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] ShootController shoot;
    [SerializeField] BossMove move;

    private void Start()
    {
        shoot.PickUp(GunConfig.Type.pistol);
        StartCoroutine(PistolSkill());
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
    IEnumerator PistolSkill()
    {
        yield return StartCoroutine(RotateTo(0f));
        for ( float angle = 45f; angle <= 360f; angle += 45f )
        {
            yield return new WaitForSeconds(1f);
            shoot.Trigger();
            yield return StartCoroutine(RotateTo(angle));    
        }
        StartCoroutine(PistolSkill());
    }
    IEnumerator RotateTo(float angle)
    {
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;
        /*
        while( transform.rotation != rotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, move.RotateSpeed);
            yield return null;
        }
        */
        yield return null;
    }
}
