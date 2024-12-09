using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public GameObject PauseUi;
    public GameObject SettingsUi;
    public GameObject GameplayUi;
    public GameObject WinUi;
    public GameObject LooseUi;


    public GameObject startPanel;
    public GameObject levelsPanel;
    //public GameObject optionPanel;

    public static Action<bool> PauseAction;
    public static Action WinScreen;
    public static Action LooseScreen;

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }


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

    public void Pause()
    {
        isPaused = !isPaused;

        Time.timeScale = isPaused ? 0 : 1;

        startPanel.SetActive(!isPaused);
        PauseUi.SetActive(isPaused);
    }

    public void OpenSettings()
    {
        SettingsUi.SetActive(true);
        PauseUi.SetActive(false);
        startPanel.SetActive(false);
    }

    public void CloseSettings()
    {
        PauseUi.SetActive(true);
        SettingsUi.SetActive(false);
    }

    public void CloseOptions()
    {
        startPanel.SetActive(true);
        SettingsUi?.SetActive(false);
    }

    private void Win()
    {
        WinUi.SetActive(true);
    }

    private void Loose()
    {
        LooseUi.SetActive(true);
    }

    public void StartGame()
    {
        startPanel.SetActive(false);
        GameplayUi.SetActive(true);
        SceneManager.LoadScene(1);
    }

    public void ShowLevels()
    {
        startPanel.SetActive(false);
        levelsPanel.SetActive(true);
    }

    public void CloseLevels()
    {
        startPanel.SetActive(true);
        levelsPanel.SetActive(false);
    }

   

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        PauseUi.SetActive(false);
    }

    public void RestartLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);

        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0); 
    }

    public void ExitGame()
    {
        Application.Quit();


#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
