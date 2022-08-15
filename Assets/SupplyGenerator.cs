using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyGenerator : MonoBehaviour
{
    [SerializeField] private GameObject pistolPrefab, medicalKitPrefab;
    [SerializeField] private Camera cam;
    [SerializeField] private GameEvent supplyDrop;
    [SerializeField] private int offset;
    [SerializeField] private float generateTime;

    private void Start()
    {
        StartCoroutine(CreateSupply());
    }
    IEnumerator CreateSupply()
    {
        yield return new WaitForSeconds(generateTime);
        int axisX = Random.Range(offset, Screen.width-offset);
        int axisY = Random.Range(offset, Screen.height-offset);
        Vector2 pos = cam.ScreenToWorldPoint(new Vector3(axisX, axisY, 0f));
        Instantiate((int)axisX%3==0?medicalKitPrefab:pistolPrefab,pos,Quaternion.identity);
        supplyDrop.Raise();
        StartCoroutine(CreateSupply());
    }
}
