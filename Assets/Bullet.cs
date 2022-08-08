using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    [SerializeField] private BulletConfig config;
    [SerializeField] private Rigidbody2D rb;
    
    private CameraShake camShake;
    private ParticleSystem hitParticle;
    public BulletConfig Config { get { return config; } private set { value = config; } }
    public Rigidbody2D Rb { get { return rb; } private set { value = rb; } }

    public void Initial()
    {
        Camera.main.TryGetComponent(out camShake);    
        hitParticle = Instantiate(config.HitParticlePrefab);
    }

    private void OnEnable()
    {
        StartCoroutine(AutoRelease());
    }
    private void OnDisable()
    {
        rb.velocity = Vector2.zero;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    { 
        if(collider.gameObject.TryGetComponent(out Health health))
        {
            config.HitSound.source.Play();
            health.TakeDamage(config.Damage);
            camShake.Shake();
            hitParticle.transform.position = transform.position;
            hitParticle.Play();
            BulletPoolController.Instance.Release(this);
        }     
    }
    IEnumerator AutoRelease()
    {
        yield return new WaitForSeconds(15f);
        BulletPoolController.Instance.Release(this);
    }
}
