using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private BulletConfig config;
    [SerializeField] private Sound hitSound;

    private Rigidbody2D rb;
    private CameraShake camShake;
    private ParticleSystem hitParticle;

    private void Start()
    {
        TryGetComponent(out rb);
        Camera.main.TryGetComponent(out camShake);
        hitParticle = Instantiate(config.HitParticlePrefab);
    }

    private void OnEnable()
    {
        rb.AddForce(transform.right*config.Force);
    }
    private void OnDisable()
    {
        rb.velocity = Vector2.zero;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    { 
        if(collider.gameObject.TryGetComponent(out Health health))
        {
            hitSound.source.Play();
            health.TakeDamage(config.Damage);
            camShake.Shake();
            hitParticle.transform.position = transform.position;
            hitParticle.Play();
            gameObject.SetActive(false);
        }     
    }
}
