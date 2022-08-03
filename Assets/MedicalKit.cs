using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicalKit : MonoBehaviour
{
    [SerializeField] private ParticleSystem healParticle;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.TryGetComponent(out Health health))
        {
            health.Heal(50);
            //play particle
            Destroy(gameObject);
        }
    }
}
