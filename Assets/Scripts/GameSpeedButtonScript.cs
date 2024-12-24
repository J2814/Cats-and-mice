using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSpeedButtonScript : MonoBehaviour
{
    private Text text;
    void Start()
    {
        text = GetComponentInChildren<Text>();
    }

    void Update()
    {
        if (GameStateManager.instance.CurretPlayTimeScale == 1)
        {
            text.text = ">";
        }
        if (GameStateManager.instance.CurretPlayTimeScale == 1.5)
        {
            text.text = ">>";
        }
        if (GameStateManager.instance.CurretPlayTimeScale == 2)
        {
            text.text = ">>>";
        }

    }
}
