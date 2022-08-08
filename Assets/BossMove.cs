using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    [SerializeField] private float speed, rotateSpeed;
    [SerializeField] private LayerMask supplyLayer, playerLayer;
    [SerializeField] private Rigidbody2D rb;
    
    private Transform targetTransform;
    private Vector3 direction;

    private void FixedUpdate()
    {
        if( targetTransform == null || Vector2.Distance(targetTransform.position,transform.position)<.1f )
        {
            LocatePlayer();
        }
        direction = (targetTransform.position - transform.position).normalized;
        Rotate();
        Move();
    }
    private void Rotate()
    {       
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed);
    }
    private void Move()
    {
        rb.MovePosition(transform.position+speed*Time.deltaTime*direction);
    }
    public void LocatePlayer()
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
