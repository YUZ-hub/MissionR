using System.Collections;
using UnityEngine;

public class BossShoot : ShootController
{
    [SerializeField] private GameObject leftHand;
    [SerializeField] private BossController controller;
    [SerializeField] private float aimTime, prepareTime;
    [SerializeField] private int shootTimes;
    [SerializeField] private float ultShootTime, ultInterval, ultPrepareTime;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject ultMissile;
    [SerializeField] private int ultMissileNums;
    [SerializeField] private Sound ultMissileShootSound;


    public override void PickUp(GunConfig.Type type)
    {
        base.PickUp(type);
        leftHand.SetActive(false);
    }
    public override void Trigger()
    {
        base.Trigger();
        if( gun.IsEmpty())
        {
            gun.gameObject.SetActive(false);
            leftHand.SetActive(true);
            gun = null;
        }
    }
    public void NormalShoot()
    {
        StartCoroutine(NormalShootSequence());
    }
    private void Flip()
    {
        Vector3 temp = transform.localScale;
        temp.x *= -1;
        transform.localScale = temp;
    }
    private void Aim()
    {
        Vector2 direction = playerTransform.position - gunTransform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (direction.x > 0 ^ transform.localScale.x > 0)
        {
            Flip();
        }
        if (direction.x < 0)
        {
            angle += 180;
        }
        gunTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);   
    }
    IEnumerator NormalShootSequence()
    {
        Quaternion originalRotation = gunTransform.rotation;
        float originalDirection = transform.localScale.x;
        for ( int i = 0; i < 3; i++)
        {
            float time = 0f;
            while (time < aimTime)
            {
                Aim();
                time += Time.deltaTime;
                yield return null;
            }
            yield return new WaitForSeconds(prepareTime);
            Trigger();
            if (originalDirection != transform.localScale.x)
            {
                Flip();
            }
        }
        gunTransform.rotation = originalRotation;
        controller.SetIdle();
    }
    public void Ultimate()
    {
        switch (gun.Config.GunType)
        {
            case GunConfig.Type.pistol:
                StartCoroutine(PistolUlt());
                break;
            case GunConfig.Type.rocket:
                StartCoroutine(RocketUlt());
                break;
            default:
                break;
        }
    }
    private IEnumerator PistolUlt()
    {
        float leftX = CameraHandler.Instance.ScreenToWorldPoint(new Vector2(100f, 0f)).x;
        transform.position = new Vector2(leftX,playerTransform.position.y);
        if(transform.localScale.x < 0)
        {
            Flip();
        }
        Quaternion originalRotation = gunTransform.rotation;
        Aim();
        yield return new WaitForSeconds(ultPrepareTime);
        float time = 0f;
        float interval = 0f;
        while ( gun!=null && gun.IsEmpty()==false && time<ultShootTime )
        {
            Vector2 direction = new Vector2(0f, playerTransform.position.y > transform.position.y ? 1 : -1);
            transform.Translate(Time.deltaTime*controller.Move.Speed*direction);
            time += Time.deltaTime;
            interval -= Time.deltaTime;
            if (interval <= 0f)
            {
                Trigger();
                interval = ultInterval;
            }
            yield return null;
        }
        gunTransform.rotation = originalRotation;
        controller.SetIdle();
    }
    private IEnumerator RocketUlt()
    {
        Quaternion originalRotation = gunTransform.rotation;
        gunTransform.rotation = Quaternion.AngleAxis(-90f, Vector3.forward);
        for( int i = 0; i < ultMissileNums; i++)
        {
            ultMissileShootSound.source.Play();
            Instantiate(ultMissile, gun.ShootPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(ultInterval);
        }
        gunTransform.rotation = originalRotation;
        controller.SetIdle();
    }
}
