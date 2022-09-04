using UnityEngine;
using System.Collections;

public class Supply : MonoBehaviour
{
    [SerializeField] private float dropOffset;
    [SerializeField] private Sound dropSound;
    [SerializeField] private GunConfig.Type type;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnPickedUpBy(collision);
    }
    protected virtual void OnPickedUpBy(Collider2D collision)
    {
        if (collision.TryGetComponent(out ShootController shoot))
        {
            shoot.PickUp(type);
            Destroy(gameObject);
        }
    }
    private void OnEnable()
    {
        StartCoroutine(DropFromTop()); 
    }
    protected IEnumerator DropFromTop()
    {
        if (dropOffset == 0)
        {
            yield break;
        }
        Vector2 originalPos = transform.position;
        Vector2 newPos = originalPos;
        newPos.y += dropOffset;

        float time = 0;
        dropSound.Play();
        while (time < 1f)
        {
            transform.position = Vector2.Lerp(newPos, originalPos, time);
            time += Time.deltaTime;
            yield return null;
        }
    }
}
