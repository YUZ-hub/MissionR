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

    private bool waitForIdle = false;
    private bool isIdle = false;
    public bool IsIdle { get { return isIdle; } private set { value = isIdle; } }
    public BossMove Move { get { return move; } private set { value = move; } }
    IEnumerator Start()
    {
        yield return new WaitForSeconds(breakTime);
        SetIdle();
    }
    private void Update()
    {
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
                    move.Patrol();
                    break;
                case 1:
                    if (shoot.gun != null)
                        shoot.NormalShoot();
                    else
                        DashSmite();
                    break;
                case 2:
                    if (shoot.gun != null)
                        shoot.Ultimate();
                    else
                        DashSmite();
                    break;
                default:
                    if (shoot.gun != null)
                        shoot.NormalShoot();
                    else
                        DashSmite();
                    break;
            }
        }
    }
    private void DashSmite()
    {
        move.DashToPlayerBack();
        animator.Play("Smash");
    }
    public void SmiteDamageEvent()
    {
        Collider2D hit = Physics2D.OverlapCircle(smiteTransform.position, smiteRange, playerLayer);
        if( hit && hit.TryGetComponent(out Health health) )
        {
            health.TakeDamage(40);
        }
        smashSound.source.Play();
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
            move.MoveTo(hit.transform);
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
}