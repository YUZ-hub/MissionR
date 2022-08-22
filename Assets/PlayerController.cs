using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private ShootController shoot;
    [SerializeField] private Animator animator;

    public bool isAlive { get; private set; } = true;

    private void Start()
    {
        shoot.PickUp(GunConfig.Type.pistol);
    }
    void Update()
    {
        if( isAlive == false)
        {
            return;
        }
        if( Input.GetMouseButton(0) )
        {
            shoot.Trigger();
        }        
    }
    public void OnPlayerDie()
    {
        animator.Play("Die");
        isAlive = false;
    }

}
