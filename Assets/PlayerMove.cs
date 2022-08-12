using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed, rotateSpeed;
    [SerializeField] private Camera cam;
    [SerializeField] private Transform gunHoldTransform;

    private Vector2 destination, mousePos;
    private Rigidbody2D rb;

    private void Start()
    {
        TryGetComponent(out rb);
        destination = transform.position;
    }


    private void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Rotate();
        if ( Input.GetMouseButtonDown(1) )
        {
            destination = mousePos;
        }
    }
    private void FixedUpdate()
    {
        if( Vector2.Distance(transform.position, destination) > .3f )
        {
            Move();
        }
    }

    private void Rotate()
    {
        Vector2 direction = (mousePos - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;    
        if ((direction.x * transform.localScale.x) < 0)
        {
            Flip();
        }
        if( direction.x<0)
        {
            angle += 180;
        }
        gunHoldTransform.rotation = Quaternion.Slerp(gunHoldTransform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), rotateSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), rotateSpeed);
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
        rb.MovePosition((Vector2)transform.position + speed*Time.fixedDeltaTime*direction);   
    }
}
