using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] ShootController shootController;

    void Update()
    {
        if( Input.GetMouseButton(0) )
        {
            shootController.Trigger();
        }        
    }
}
