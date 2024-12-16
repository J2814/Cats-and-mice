using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystemDontDestroy : MonoBehaviour
{

    private static EventSystemDontDestroy instance;
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
