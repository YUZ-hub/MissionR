using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private ShootController shoot;
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerMove move;

    private bool isAlive = true;

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
        Vector2 mousePos = CameraHandler.Instance.ScreenToWorldPoint(Input.mousePosition);
        move.RotateTo(mousePos);
        if (Input.GetMouseButtonDown(1))
        {
            move.MoveTo(mousePos);
        }
        else
        {
            move.Move();
        }
    }
    public void OnPlayerDie()
    {
        animator.Play("Die");
        isAlive = false;
    }

}
