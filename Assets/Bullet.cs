using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private BulletConfig config;
    [SerializeField] private Rigidbody2D rb;
    private CameraShake camShake;

    private void Start()
    {
        Camera.main.TryGetComponent(out camShake);
    }

    private void OnEnable()
    {
        rb.velocity = transform.right * config.Speed;
    }
    private void OnDisable()
    {
        rb.velocity = Vector2.zero;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if( (config.TargetLayer&(1<<collider.gameObject.layer)) > 0)
        {
            if(collider.gameObject.TryGetComponent(out Health health))
            {
                health.TakeDamage(config.Damage);
                camShake.Shake();
                //play particle
                gameObject.SetActive(false);
            }     
        }
    }
}
