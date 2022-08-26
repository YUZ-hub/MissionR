using UnityEngine;

public class MedicalKit : Supply
{
    [SerializeField] private ParticleSystem healParticlePrefab;
    [SerializeField] private Sound healSound;
    private ParticleSystem healParticle;

    private void Start()
    {
        healParticle = Instantiate(healParticlePrefab);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnPickedUpBy(collision);
    }
    protected override void OnPickedUpBy(Collider2D collision)
    {
        if (collision.TryGetComponent(out Health health))
        {
            health.Heal(.3f);
            healSound.Play();
            healParticle.transform.position = transform.position;
            healParticle.Play();
            Destroy(gameObject);
        }
    }
    private void OnEnable()
    {
        StartCoroutine(DropFromTop());
    }
}
