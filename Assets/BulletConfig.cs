using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New BulletConfig", menuName ="Config/Bullet")]
public class BulletConfig : ScriptableObject
{
    [SerializeField] private int damage;
    [SerializeField] private float force;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private ParticleSystem hitParticlePrefab;

    public int Damage { get { return damage; } private set { value = damage; } }
    public float Force { get { return force; } private set { value = force; } }
    public LayerMask TargetLayer { get { return targetLayer; } private set { value = targetLayer; } }
    public ParticleSystem HitParticlePrefab { get { return hitParticlePrefab; } private set { value = hitParticlePrefab; } }

}
