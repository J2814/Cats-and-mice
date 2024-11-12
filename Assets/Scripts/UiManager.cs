using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public GameObject PauseUi;
    public GameObject SettingsUi;
    public GameObject GameplayUi;
    public GameObject WinUi;
    public GameObject LooseUi;

    public static Action<bool> PauseAction;
    public static Action WinScreen;
    public static Action LooseScreen;

    private void OnEnable()
    {
        PauseAction += Pause;
        WinScreen += Win;
        LooseScreen += Loose;
    }
    private void OnDisable()
    {
        PauseAction -= Pause;
        WinScreen -= Win;
        LooseScreen -= Loose;
    }

    public void Pause(bool isPaused)
    {
        if (isPaused)
        {
            PauseUi.SetActive(true);
            SettingsUi.SetActive(false);
        }
        else
        {
            PauseUi.SetActive(false);
            SettingsUi.SetActive(false);
        }
    }

    public void OpenSettings()
    {
        SettingsUi.SetActive(true);
        PauseUi.SetActive(false);
    }

    public void CloseSettings()
    {
        PauseUi.SetActive(true);
        SettingsUi.SetActive(false);
    }

    private void Win()
    {
        WinUi.SetActive(true);
    }

    private void Loose()
    {
        LooseUi.SetActive(true);
    }
}
