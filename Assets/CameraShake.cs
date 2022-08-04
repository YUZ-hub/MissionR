using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float duration, magnitude;
    [SerializeField] private AnimationCurve curve;
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
            float finalMagnitude = magnitude * curve.Evaluate(time / duration);
            float shakeX = Random.Range(-1f, 1f) * finalMagnitude;
            float shakeY = Random.Range(-1f, 1f) * finalMagnitude;
            transform.position = new Vector3(originalPos.x+shakeX,originalPos.y+shakeY,originalPos.z);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = originalPos;
    }
}
