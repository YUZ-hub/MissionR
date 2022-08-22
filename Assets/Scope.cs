using UnityEngine;

public class Scope : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private ParticleSystem particlePrefab;
    [SerializeField] private Sound explosionSound;
    [SerializeField] private int damage;
    [SerializeField] private LayerMask targetLayer;
    public void Trigger()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, range);
        if( hit && hit.TryGetComponent(out Health health))
        {
            health.TakeDamage(damage);    
        }
        ParticleSystem particle = Instantiate(particlePrefab, transform.position, Quaternion.identity);
        particle.Play();
        explosionSound.source.Play();
        Destroy(gameObject);
    }
}
