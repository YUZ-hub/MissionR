using System.Collections;
using UnityEngine;

public class SupplyGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] gunPrefabs;
    [SerializeField] private GameObject medicalKitPrefab;
    [SerializeField] private GameEvent supplyDrop, healFull;
    [SerializeField] private float generateTime;
    [SerializeField] private Health playerHealth;

    private void Start()
    {
        StartCoroutine(CreateSupply());
    }
    IEnumerator CreateSupply()
    {
        yield return new WaitForSeconds(generateTime);
        Vector2 pos = CameraHandler.Instance.RandomWorldPoint();
        int dice = Random.Range(1, 6);
        switch (dice)
        {
            case 5:
                Instantiate(medicalKitPrefab, pos, Quaternion.identity);
                if( playerHealth.Hp == playerHealth.MaxHp)
                {
                    healFull.Raise();
                }
                break;
            default:
                Instantiate(gunPrefabs[Random.Range(0, gunPrefabs.Length)], pos, Quaternion.identity);
                break;
        }

        
        supplyDrop.Raise();
        StartCoroutine(CreateSupply());
    }
}
