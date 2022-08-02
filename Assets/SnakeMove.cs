using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMove : MonoBehaviour
{
    [SerializeField] private float speed, rotateSpeed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask supplyLayer, playerLayer;

    private Transform targetTransform;

    private void Start()
    {
        LocatePlayer();
    }
    private void FixedUpdate()
    {
        if( targetTransform == null || Vector2.Distance(targetTransform.position,transform.position)<.1f )
        {
            LocatePlayer();
        }
        Move();
    }
    private void Move()
    {
        Vector2 direction = (targetTransform.position - transform.position).normalized;
        rb.MovePosition((Vector2)transform.position + speed*Time.fixedDeltaTime*direction);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed);
    }
    private void LocatePlayer()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, 100f , playerLayer);
        targetTransform = hit.transform;
    }
    public void LocateSupply()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, 100f, supplyLayer);
        targetTransform = hit.transform;
    }

}
