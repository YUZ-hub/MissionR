using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolController : MonoBehaviour
{
    [SerializeField] private GameObject objPrefab;
    [SerializeField] private int size;
    private Queue<GameObject> pool;

    private void Start()
    {
        pool = new Queue<GameObject>();
        for( int i = 0; i < size; i += 1)
        {
            GameObject obj = Instantiate(objPrefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public void GenerateTo(Vector3 position,Quaternion rotation)
    {        
        GameObject obj = pool.Dequeue();
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.SetActive(true);
        pool.Enqueue(obj);
    }
}
