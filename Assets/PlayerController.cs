using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] ShootController shoot;
    [SerializeField] Animator animator;
    private void Start()
    {
        shoot.PickUp(GunConfig.Type.pistol);
    }
    void Update()
    {
        if( Input.GetMouseButton(0) )
        {
            shoot.Trigger();
        }        
    }
    public void OnPlayerDie()
    {
        animator.Play("Die");
        //lock control of player
    }

}
