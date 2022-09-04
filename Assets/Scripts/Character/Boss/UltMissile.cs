using System.Collections;
using UnityEngine;

public class UltMissile : MonoBehaviour
{
    [SerializeField] private Scope scopePrefab;
    [SerializeField] private float riseTime,riseSpeed;
    [SerializeField] private Rigidbody2D rb;

    private Vector2 dropPoint;
    private Scope scope;

    private void OnEnable()
    {
        transform.localScale = new Vector3(1f,1f,1f);
        StartCoroutine(Rise());
    }
    IEnumerator Rise()
    {
        rb.velocity = new Vector2(0, riseSpeed);
        dropPoint = CameraHandler.Instance.RandomWorldPoint();
        scope = Instantiate(scopePrefab, dropPoint, Quaternion.identity);   
        yield return new WaitForSeconds(riseTime);
        StartCoroutine(Drop());
    }
    IEnumerator Drop()
    {
        transform.localScale = new Vector3(1f, -1f, 1f);
        rb.velocity = Vector2.down*riseSpeed;
        transform.position = new Vector2(dropPoint.x, Random.Range(20f, 40f));
        while (transform.position.y - scope.transform.position.y > .5f)
        {
            yield return null;
        }
        scope.Trigger();
        Destroy(gameObject);
    }
}
