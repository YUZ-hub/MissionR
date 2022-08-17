using System.Collections;
using UnityEngine;

public class BossShoot : ShootController
{
    [SerializeField] private GameObject leftHand;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private BossController controller;
    [SerializeField] private float aimTime, prepareTime;

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
    IEnumerator NormalShootSequence()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, 100f, playerLayer);
        if( !hit)
        {
            controller.SetIdle();
            yield break;
        }
        Transform playerTransform = hit.transform;
        float time = 0f;
        Quaternion originalRotation = gunTransform.rotation;
        float originalDirection = transform.localScale.x;
        while (time < aimTime)
        {
            Vector2 direction = playerTransform.position - gunTransform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            if (direction.x > 0 ^ transform.localScale.x > 0)
            {
                Vector3 temp = transform.localScale;
                temp.x *= -1;
                transform.localScale = temp;
            }
            if (direction.x < 0)
            {
                angle += 180;
            }
            gunTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            time += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(prepareTime);
        Trigger();
        if (originalDirection != transform.localScale.x)
        {
            Vector3 temp = transform.localScale;
            temp.x *= -1;
            transform.localScale = temp;
        }
        gunTransform.rotation = originalRotation;
        controller.SetIdle();
    }
}
