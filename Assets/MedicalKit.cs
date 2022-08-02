using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicalKit : MonoBehaviour
{
    [SerializeField] private GameObject healParticle;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.TryGetComponent(out Health health))
        {
            health.Heal(50);
            Instantiate(healParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
