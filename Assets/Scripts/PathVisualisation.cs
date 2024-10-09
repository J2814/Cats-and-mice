using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathVisualisation : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private MovementPath path;
    void Start()
    {
        path = GetComponent<MovementPath>();
        InitLineRenderer();
    }

    private void InitLineRenderer()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();
        lineRenderer.positionCount = path.PathElements.Length;

        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            lineRenderer.SetPosition(i, path.PathElements[i].position);
        }
    }
}
