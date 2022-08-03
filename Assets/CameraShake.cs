using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float duration, magnitude;

    public void Shake()
    {
        StartCoroutine(ShakeCoroutine());
    }
    
    IEnumerator ShakeCoroutine()
    {
        Vector3 originalPos = transform.position;
        float time = 0f;
        while( time < duration )
        {
            float shakeX = Random.Range(-1f, 1f) * magnitude;
            float shakeY = Random.Range(-1f, 1f) * magnitude;
            transform.position = new Vector3(originalPos.x+shakeX,originalPos.y+shakeY,originalPos.z);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = originalPos;
    }
}
