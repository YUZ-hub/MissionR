using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyGenerator : MonoBehaviour
{
    [SerializeField] private GameObject magazinePrefab, medicalKitPrefab;
    [SerializeField] private Camera cam;
    [SerializeField] private GameEvent supplyDrop;

    private void Start()
    {
        StartCoroutine(CreateSupply());
    }
    IEnumerator CreateSupply()
    {
        yield return new WaitForSeconds(10f);
        int axisX = Random.Range(0, Screen.width);
        int axisY = Random.Range(0, Screen.height);
        Vector2 pos = cam.ScreenToWorldPoint(new Vector3(axisX, axisY, 0f));
        Instantiate(axisX%2==0?magazinePrefab:medicalKitPrefab,pos,Quaternion.identity);
        supplyDrop.Raise();
        StartCoroutine(CreateSupply());
    }
}
