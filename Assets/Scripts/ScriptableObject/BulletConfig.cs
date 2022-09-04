using UnityEngine;

[CreateAssetMenu(fileName ="New BulletConfig", menuName ="Config/Bullet")]
public class BulletConfig : ScriptableObject
{
    [SerializeField] private int damage;
    [SerializeField] private float force;
    [SerializeField] private Sound hitSound;
    [SerializeField] private ParticleSystem hitParticlePrefab;

    public int Damage { get { return damage; } private set { value = damage; } }
    public float Force { get { return force; } private set { value = force; } }
    public ParticleSystem HitParticlePrefab { get { return hitParticlePrefab; } private set { value = hitParticlePrefab; } }
    public Sound HitSound { get { return hitSound; } private set { value = hitSound; } }
}
