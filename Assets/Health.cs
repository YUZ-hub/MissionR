using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHp;
    private int hp;

    public int MaxHp { get { return maxHp; } private set { value = maxHp; } }
    public int Hp { get { return hp; } private set { value = hp; } }

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
        if( hp>maxHp)
        {
            hp = maxHp;
        }
    }
    public void Heal( float _hp)
    {
        hp += Mathf.FloorToInt(maxHp * _hp);
        if( hp>maxHp)
        {
            hp = maxHp;
        }
    }
}
