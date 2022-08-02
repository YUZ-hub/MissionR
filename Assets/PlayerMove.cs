using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed, rotateSpeed;
    [SerializeField] private Camera cam;
    [SerializeField] private Rigidbody2D rb;

    private Vector2 destination, mousePos;

    private void Start()
    {
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
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed);
    }

    private void Move()
    {
        Vector2 direction = (destination - (Vector2)transform.position).normalized;
        rb.MovePosition((Vector2)transform.position + speed*Time.fixedDeltaTime*direction);   
    }
}
