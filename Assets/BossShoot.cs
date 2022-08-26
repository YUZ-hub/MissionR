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
    [SerializeField] private GameEvent bossPickUp;

    public void PowerUp()
    {
        aimTime /= 1.2f;
        prepareTime /= 1.2f;
        shootTimes += 2;
        ultShootTime *= 1.2f;
        ultInterval /= 1.2f;
        ultPrepareTime /= 1.2f;
        ultMissileNums += 5;
    }

    public override void PickUp(GunConfig.Type type)
    {
        base.PickUp(type);
        leftHand.SetActive(false);
        bossPickUp.Raise();
    }
    public override void Trigger()
    {
        base.Trigger();
        if( gun!=null && gun.IsEmpty())
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
    private void Aim()
    {
        Vector2 direction = playerTransform.position - gunTransform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (direction.x > 0 ^ transform.localScale.x > 0)
        {
            controller.Move.Flip();
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
        for ( int i = 0; i < shootTimes; i++)
        {
            if( gun == null || gun.IsEmpty())
            {
                break;
            }
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
                controller.Move.Flip();
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
            controller.Move.Flip();
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
            ultMissileShootSound.Play();
            Instantiate(ultMissile, gun.ShootPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(ultInterval);
        }
        gunTransform.rotation = originalRotation;
        controller.SetIdle();
    }
}
