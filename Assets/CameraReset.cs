using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraReset : MonoBehaviour
{
    private Camera cam;
    void Start()
    {
        cam = GetComponent<Camera>();
        CameraResetPlease();
    }

    /// <summary>
    /// i don't know why, but after disabling and enabling everything works like i want.
    /// </summary>
    private void CameraResetPlease()
    {
        cam.gameObject.SetActive(false);
        cam.gameObject.SetActive(true);
    }
}
