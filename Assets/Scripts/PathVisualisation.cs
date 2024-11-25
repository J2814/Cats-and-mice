using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathVisualisation : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private MovementPath path;

    [SerializeField]
    [Tooltip("Will be overwritten if there is PathVisualisationSettings present in the scene")]
    private Color pathColor = Color.white;
    [SerializeField]
    [Tooltip("Will be overwritten if there is PathVisualisationSettings present in the scene")]
    private Color intersectionActiveColor = Color.blue;
    [SerializeField]
    [Tooltip("Will be overwritten if there is PathVisualisationSettings present in the scene")]
    private Color intersectionOffColor = Color.red;

    [SerializeField]
    [Tooltip("Will be overwritten if there is PathVisualisationSettings present in the scene")]
    private float width = 0.2f;

    [SerializeField]
    [Tooltip("Will be overwritten if there is PathVisualisationSettings present in the scene")]
    private Material material;

    private float Yoffset = 0;


    void Start()
    {
        
        path = GetComponent<MovementPath>();
        GetSettings();
        InitLineRenderer();
        
    }

    private void GetSettings()
    {
        if (PathVisualisationSettings.instance != null)
        {
            pathColor = PathVisualisationSettings.instance.PathColor;
            intersectionActiveColor = PathVisualisationSettings.instance.IntersectionActiveColor;
            intersectionOffColor = PathVisualisationSettings.instance.IntersectionOffColor;
            width = PathVisualisationSettings.instance.Width;
            Yoffset = PathVisualisationSettings.instance.Yoffset;
            material = PathVisualisationSettings.instance.Material;
        }
    }

    private void InitLineRenderer()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();
        lineRenderer.material = material;
        lineRenderer.positionCount = path.PathElements.Length;
        lineRenderer.startWidth = width;
        lineRenderer.colorGradient = ColorSetUp();
        lineRenderer.sortingOrder = 1;

        LineRenderer offPathLineRenderer = null;
        if (GetComponent<IntersectionPath>() != null)
        {
            GameObject lineObject = new GameObject("OffLineRenderer");
            offPathLineRenderer = lineObject.AddComponent<LineRenderer>();
            offPathLineRenderer.material = material;
            offPathLineRenderer.positionCount = path.PathElements.Length;
            offPathLineRenderer.startWidth = width;
            offPathLineRenderer.colorGradient = ColorGradient(intersectionOffColor);
            offPathLineRenderer.sortingOrder = -1;
        }

        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            Vector3 pos = new Vector3(path.PathElements[i].position.x, path.PathElements[i].position.y + Yoffset, path.PathElements[i].position.z);
            lineRenderer.SetPosition(i, pos);

            if (GetComponent<IntersectionPath>() != null)
            {
                offPathLineRenderer.SetPosition(i, pos);
            }
        }
    }

    private Gradient ColorSetUp()
    {
        Gradient gradient = ColorGradient(pathColor);

        if (GetComponent<IntersectionPath>() != null)
        {
            gradient = ColorGradient(intersectionActiveColor);
        }
        return gradient;
    }
    private Gradient ColorGradient(Color c)
    {
        Gradient gradient = new Gradient();

        gradient.colorKeys = new GradientColorKey[]
        {
            new GradientColorKey(c, 0f),  
            new GradientColorKey(c, 1f)   
        };

        gradient.alphaKeys = new GradientAlphaKey[]
        {
            new GradientAlphaKey(1f, 0f),       
            new GradientAlphaKey(1f, 1f)        
        };

        
        return gradient;
    }
}
