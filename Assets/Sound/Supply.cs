using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supply : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
         if( collision.gameObject.TryGetComponent(out ShootController shoot))
         {
              shoot.Reload();
              Destroy(gameObject);
         }
    }
}
