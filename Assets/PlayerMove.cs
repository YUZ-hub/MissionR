using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed, rotateSpeed;
    [SerializeField] private Transform gunHoldTransform;
    [SerializeField] private PlayerController controller;

    private Vector2 destination, mousePos;

    private void Start()
    {
        destination = transform.position;
    }
    private void Update()
    {
        if( controller.isAlive == false)
        {
            return;
        }
        mousePos = CameraHandler.Instance.ScreenToWorldPoint(Input.mousePosition);
        Rotate();
        if ( Input.GetMouseButtonDown(1) )
        {
            destination = mousePos;
        }
        if (Vector2.Distance(transform.position, destination) > .3f)
        {
            Move();
        }
    }

    private void Rotate()
    {
        Vector2 direction = (mousePos - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;    
        if (direction.x>0^transform.localScale.x>0)
        {
            Flip();
        }
        if( direction.x<0)
        {
            angle += 180;
        }
        gunHoldTransform.rotation = Quaternion.Slerp(gunHoldTransform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), rotateSpeed);
    }
    private void Flip()
    {
        Vector3 temp = transform.localScale;
        temp.x *= -1;
        transform.localScale = temp;
    }
    private void Move()
    {
        Vector2 direction = (destination - (Vector2)transform.position).normalized;
        transform.Translate(speed*Time.deltaTime*direction);   
    }
}
