using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public static CameraHandler Instance { get; private set; }
    [SerializeField] private Camera cam;
    [SerializeField] private float duration, magnitude;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private int offset;
    
    private void Awake()
    {
        if( Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        SetCamera();
    }
    public void SetCamera(Camera _cam = null)
    {
        if( _cam == null)
        {
            cam = Camera.main;
            return;
        }
        cam = _cam;
    }
    public Vector2 ScreenToWorldPoint(Vector2 screenPos)
    {
        if( cam == null )
            SetCamera();
        return cam.ScreenToWorldPoint(screenPos);
    }
    public Vector2 RandomWorldPoint()
    {
        if (cam == null)
            SetCamera();
        int axisX = Random.Range(offset, Screen.width - offset);
        int axisY = Random.Range(offset, Screen.height - offset);
        return cam.ScreenToWorldPoint(new Vector2(axisX, axisY));
    }
    public void Shake()
    {
        if (cam == null)
            SetCamera();
        StartCoroutine(ShakeSequence());
    }
    IEnumerator ShakeSequence()
    {
        Vector3 originalPos = transform.position;
        float time = 0f;
        while (time < duration)
        {
            float finalMagnitude = magnitude * curve.Evaluate(time / duration);
            float shakeX = Random.Range(-1f, 1f) * finalMagnitude;
            float shakeY = Random.Range(-1f, 1f) * finalMagnitude;
            transform.position = new Vector3(originalPos.x + shakeX, originalPos.y + shakeY, originalPos.z);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = originalPos;
    }
}
