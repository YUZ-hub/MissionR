using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicalKit : MonoBehaviour
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
        if ( collision.TryGetComponent(out Health health))
        {
            health.Heal(50);
            healSound.source.Play();
            healParticle.transform.position = transform.position;
            healParticle.Play();
            Destroy(gameObject);
        }
    }
}
