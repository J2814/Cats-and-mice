using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VisualSettings : MonoBehaviour
{
    public static VisualSettings instance;

    public Color IntersectionActiveColor;
    public Color IntersectionOffColor;
    public Color PathColor;

    public float PathYoffset;

    public float PathWidth;

    public Material PathMaterial;

    public float IntersectionTextPunchScale = 1.25f;

    public Color IntersectionColorPunch;


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
