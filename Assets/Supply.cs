using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supply : MonoBehaviour
{
    [SerializeField] private GunConfig.Type type;

    private void OnTriggerEnter2D(Collider2D collision)
    {
         if( collision.gameObject.TryGetComponent(out ShootController shoot))
         {
              shoot.PickUp(type);
              Destroy(gameObject);
         }
    }
}
