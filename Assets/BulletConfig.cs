using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New BulletConfig", menuName ="Config/Bullet")]
public class BulletConfig : ScriptableObject
{
    [SerializeField] private int damage;
    [SerializeField] private float speed;
    [SerializeField] private LayerMask targetLayer;

    public int Damage { get { return damage; } set { value = damage; } }
    public float Speed { get { return speed; } set { value = speed; } }
    public LayerMask TargetLayer { get { return targetLayer; } set { value = targetLayer; } }

}
