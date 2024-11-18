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
        lineRenderer.startWidth = 0.2f;
    }

    private void InitLineRenderer()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();
        lineRenderer.positionCount = path.PathElements.Length;

        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            Vector3 pos = new Vector3(path.PathElements[i].position.x, path.PathElements[i].position.y - 0.5f, path.PathElements[i].position.z);
            lineRenderer.SetPosition(i, pos);
        }
    }
}
