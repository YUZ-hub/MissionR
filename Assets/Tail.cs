using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tail : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private int length;
    [SerializeField] private Transform tailDir;
    [SerializeField] private float tailLength, tailSmooth;

    private Vector3[] nodePoses, nodeVs;

    private void Start()
    {
        lineRenderer.positionCount = length;
        nodePoses = new Vector3[length];
        nodeVs = new Vector3[length];
    }
    private void Update()
    {
        nodePoses[0] = tailDir.position;
        for( int i = 1; i < nodePoses.Length; i += 1)
        {
            nodePoses[i] = Vector3.SmoothDamp(nodePoses[i], nodePoses[i - 1]+tailLength*tailDir.right, ref nodeVs[i], tailSmooth);
        }
        lineRenderer.SetPositions(nodePoses);
    }
}
