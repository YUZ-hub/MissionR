using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHp;
    private int hp;
    [SerializeField] private GameEvent dieEvent;
    public int MaxHp { get { return maxHp; } private set { maxHp = value; } }
    public int Hp { get { return hp; } private set { hp = value; } }

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
        if(dieEvent!=null)    
            dieEvent.Raise();
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
