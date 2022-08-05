using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

abstract public class ShootController : MonoBehaviour
{
    abstract protected void Trigger();
    abstract public void Reload();
}
