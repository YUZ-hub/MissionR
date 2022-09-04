using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private BossShoot shoot;
    [SerializeField] private BossMove move;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform smiteTransform;
    [SerializeField] private LayerMask playerLayer, supplyLayer;
    [SerializeField] private float smiteRange, breakTime, detectRange;
    [SerializeField] private Sound smashSound;
    [SerializeField] private Health health;

    private bool waitForIdle = false;
    private bool isIdle = false;
    private bool isRage = false;

    public BossMove Move { get { return move; } private set { move = value; } }
    public BossShoot Shoot { get { return shoot; } private set { shoot = value; } }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(breakTime);
        SetIdle();
    }
    private void Update()
    {
        if( isRage == false && health.Hp<health.MaxHp/2)
        {
            isRage = true;
            Rage();
        }

        if( isIdle )
        {
            if (waitForIdle)
            {
                waitForIdle = false;
                FindSupply();
                return;
            }

            isIdle = false;
            int dice = Random.Range(0, 3);
            switch (dice)
            {
                case 0:
                    Move.Patrol();
                    break;
                case 1:
                    if (shoot.gun != null)
                        Shoot.NormalShoot();
                    else
                        DashSmite();
                    break;
                case 2:
                    if (Shoot.gun != null)
                        Shoot.Ultimate();
                    else
                        DashSmite();
                    break;
                default:
                    if (Shoot.gun != null)
                        Shoot.NormalShoot();
                    else
                        DashSmite();
                    break;
            }
        }
    }
    private void DashSmite()
    {
        Move.DashToPlayerBack();
        animator.Play("Smash");
    }
    public void SmiteDamageEvent()
    {
        Collider2D hit = Physics2D.OverlapCircle(smiteTransform.position, smiteRange, playerLayer);
        if( hit && hit.TryGetComponent(out Health health) )
        {
            health.TakeDamage(40);
        }
        smashSound.Play();
        CameraHandler.Instance.Shake();
        SetIdle();
    }
    public void FindSupply()
    {
        if( isIdle == false )
        {
            waitForIdle = true;
            return;
        }
        isIdle = false;
        Collider2D hit = Physics2D.OverlapCircle(transform.position, detectRange, supplyLayer);
        if( hit )
        {
            Move.MoveTo(hit.transform);
        }
        else
        {
            isIdle = true;
        }
    }
    public void SetIdle()
    {
        StartCoroutine(SetIdleDelay());
    }
    IEnumerator SetIdleDelay()
    {
        yield return new WaitForSeconds(breakTime);
        isIdle = true;
    }
    public void OnBossDie()
    {
        Destroy(Move);
        Destroy(Shoot);
        animator.Play("Die");
        Destroy(this);    
    }
    public void Rage()
    {
        move.SpeedUp();
        shoot.PowerUp();
        breakTime /= 1.5f;
        smiteRange *= 1.2f;
    }
}