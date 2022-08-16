using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] ShootController shoot;
    private void Start()
    {
        shoot.PickUp(GunConfig.Type.pistol);
    }
    void Update()
    {
        if( Input.GetMouseButton(0) )
        {
            shoot.Trigger();
        }        
    }
}
