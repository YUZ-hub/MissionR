using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMove : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Camera cam;

    private Vector2 destination;
    private Vector2 direction;

    private void Update()
    {
        if( Input.GetMouseButtonDown(1) )
        {
            destination = cam.ScreenToWorldPoint(Input.mousePosition);
        }
        if( destination != null)
        {
            Move();
        }
    }
    private void Rotate()
    {
        //calculate direction
    }

    private void Move()
    {
        Vector2 pos = transform.position;
        if( Vector2.Distance(pos, destination ) < .1f)
        {
            return;
        }
        direction = (destination - pos).normalized;
        transform.Translate(speed * Time.deltaTime * direction);
    }
}
