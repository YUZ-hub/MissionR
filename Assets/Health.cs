using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHp;
    private int hp;

    private void Start()
    {
        hp = maxHp;
    }

    public void TakeDamage( int damage )
    {
        hp -= damage;
        if( hp < 0)
        {
            hp = 0;
            Die();
        }
    }
    private void Die()
    {
        //die
    }
    public void Heal( int _hp )
    {
        hp += _hp;
    }
}
