using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    [SerializeField] private float speed, rotateSpeed;
    [SerializeField] private LayerMask supplyLayer, playerLayer;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float dashOffset, golemHeight;
    [SerializeField] private float patrolTime;
    [SerializeField] private BossController controller;
    public float RotateSpeed { get; private set; }
    public void DashToPlayerBack()
    {
        Vector2 targetPos = playerTransform.position;
        int direction = playerTransform.localScale.x < 0 ? -1 : 1;
        if( direction*transform.localScale.x<0)
        {
            Flip();
        }
        targetPos.y += golemHeight;
        targetPos -= direction*dashOffset*(Vector2)playerTransform.right;
        transform.position = targetPos;
    }
    private void Flip()
    {
        Vector3 temp = transform.localScale;
        temp.x *= -1;
        transform.localScale = temp;
    }
    public void Patrol()
    {
        StartCoroutine(PatrolSequence());
    }

    public void MoveTo(Transform target)
    {
        StartCoroutine(MoveToSequence(target));
    }
    IEnumerator MoveToSequence(Transform target)
    {
        while (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            rb.MovePosition((Vector2)transform.position +speed*Time.deltaTime*direction);
            if(direction.x * transform.localScale.x < 0)
            {
                Flip();
            }
            yield return null;
        }
        controller.SetIdle();
    }
    IEnumerator PatrolSequence()
    {
        float time = 0f;
        Vector2 direction = playerTransform.position.x > transform.position.x ? Vector2.right : Vector2.left;
        if(direction.x * transform.localScale.x < 0)
        {
            Flip();
        }
        while (time < patrolTime)
        {
            rb.MovePosition((Vector2)transform.position + Time.deltaTime * speed * direction);
            time += Time.deltaTime;
            yield return null;
        }
        controller.SetIdle();
    }


}
