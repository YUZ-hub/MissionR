using System.Collections;
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
        Vector2 pos = CameraHandler.Instance.RandomWorldPoint(offset);
        int dice = Random.Range(1, 6);
        Instantiate(dice>2?pistolPrefab:medicalKitPrefab,pos,Quaternion.identity);
        supplyDrop.Raise();
        StartCoroutine(CreateSupply());
    }
}
