using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed, rotateSpeed;
    [SerializeField] private Transform gunHoldTransform;
    [SerializeField] private PlayerController controller;

    private Vector2 destination;

    private void Start()
    {
        destination = transform.position;
    }
    public void RotateTo(Vector2 mousePos)
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
    public void MoveTo(Vector2 des)
    {
        destination = des;
        Move();
    }

    public void Move()
    {
        if (Vector2.Distance(destination, transform.position) < .3f)
        {
            return;
        }
        Vector2 direction = (destination - (Vector2)transform.position).normalized;
        transform.Translate(speed*Time.deltaTime*direction);   
    }
}
