using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PathVisualisationSettings : MonoBehaviour
{
    public static PathVisualisationSettings instance;

    public Color IntersectionActiveColor;
    public Color IntersectionOffColor;
    public Color PathColor;

    public float Yoffset;

    public float Width;

    public Material Material;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
