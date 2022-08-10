using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    [SerializeField] private float speed, rotateSpeed;
    [SerializeField] private LayerMask supplyLayer, playerLayer;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform playerTransform;

    private Vector2 destination;
    public float RotateSpeed { get; private set; }

    public void Rotate()
    {
        Vector2 direction = destination - (Vector2)transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed);
    }

    public void Aim()
    {
        destination = playerTransform.position;
        Rotate();
    }
}
