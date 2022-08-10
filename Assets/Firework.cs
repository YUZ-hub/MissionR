using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firework : MonoBehaviour
{
    [SerializeField] private BulletConfig.Type explosionBulletType;
    [SerializeField] private int bulletNum;
    [SerializeField] private float shakeTime, flyTime, magnitude;
    [SerializeField] private AnimationCurve shakeCurve;

    public void Shoot()
    {
        StartCoroutine(FlyToCenter());
    }
    private IEnumerator FlyToCenter()
    {
        float time = 0f;
        Vector2 originalPos = transform.position;
        while ( time<flyTime )
        {
            transform.position = Vector2.Lerp(originalPos, Vector2.zero, time/flyTime);
            time += Time.deltaTime;
            yield return null;
        }
        StartCoroutine(Shake());
    }
    private IEnumerator Shake()
    {
        float time = 0f;
        Vector2 originalPos = transform.position;
        while (time < shakeTime)
        {
            float finalMagnitude = magnitude * shakeCurve.Evaluate(time / shakeTime);
            float shakeX = Random.Range(-1f, 1f) * finalMagnitude;
            float shakeY = Random.Range(-1f, 1f) * finalMagnitude;
            transform.position = new Vector2(originalPos.x + shakeX, originalPos.y + shakeY);
            time += Time.deltaTime;
            yield return null;
        }
        Explosion();
        Destroy(gameObject);    
    }
    private void Explosion()
    {
        for( int angle = 0 ; angle < 360 ; angle += 360/bulletNum )
        {
            Bullet bullet = BulletPoolController.Instance.Get(explosionBulletType);    
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            bullet.Rb.AddForce(bullet.transform.right* bullet.Config.Force);
        }
    }
}
