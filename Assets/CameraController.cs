using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera cam;

    public static CameraController Instance;

    private void Awake()
    {
        if( Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        //No need to set dont destroy on load
    }
    private void Start()
    {
        if( cam == null)
        {
            cam = Camera.main;
        }
    }
    public void Shake()
    {

    }
    public void RandomWorldPoint()
    {

    }
    public void MouseToWorldPoint()
    {

    }
}
