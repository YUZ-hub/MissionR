using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    [SerializeField] private BulletConfig config;
    [SerializeField] private Rigidbody2D rb;
    
    private ParticleSystem hitParticle;
    public BulletConfig Config { get { return config; } private set { value = config; } }
    public Rigidbody2D Rb { get { return rb; } private set { value = rb; } }

    public void Initial()
    {
        hitParticle = Instantiate(config.HitParticlePrefab);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    { 
        if(collider.gameObject.TryGetComponent(out Health health))
        {
            config.HitSound.Play();
            health.TakeDamage(config.Damage);
            CameraHandler.Instance.Shake();
            hitParticle.transform.position = transform.position;
            hitParticle.Play();
            gameObject.SetActive(false);
        }     
    }
}
