using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour, IGameStateResonder
{


    public static UiManager instance;

    public GameObject PauseUi;
    public GameObject SettingsUi;
    public GameObject GameplayUi;
    public GameObject WinUi;
    public GameObject LooseUi;


    public GameObject MainMenuUi;
    public GameObject LevelSelectMenu;
    //public GameObject optionPanel;
    private void OnEnable()
    {
        GameStateManager.CurrentGameState += RespondToGameState;
    }

    private void OnDisable()
    {
        GameStateManager.CurrentGameState -= RespondToGameState;
    }

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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void RespondToGameState(GameStateManager.GameState gameState)
    {
        if (gameState == GameStateManager.GameState.WinScreen)
        {
            ShowWinScreen();
        }

        if (gameState == GameStateManager.GameState.LooseScreen)
        {
            ShowLooseScreen();
        }

        if (gameState == GameStateManager.GameState.Pause)
        {
            Pause();
        }

        if (gameState == GameStateManager.GameState.MainMenu)
        {
            MainMenu();
        }

        if (gameState == GameStateManager.GameState.Gameplay)
        {
            Gameplay();
        }
    }

    public void Pause()
    {
        PauseUi.SetActive(true);

        SettingsUi?.SetActive(false);
        MainMenuUi?.SetActive(false);
        WinUi.SetActive(false);
        LooseUi.SetActive(false);
    }
    public void Gameplay()
    {
        GameplayUi.SetActive(true);
        SettingsUi?.SetActive(false);
        MainMenuUi?.SetActive(false);
        PauseUi?.SetActive(false);
        WinUi.SetActive(false);
        LooseUi.SetActive(false);
    }
    public void OpenSettings()
    {
        SettingsUi.SetActive(true);
        PauseUi.SetActive(false);
        MainMenuUi.SetActive(false);
    }

    public void CloseSettings()
    {
        if (GameStateManager.instance.MyGameState == GameStateManager.GameState.MainMenu)
        {
            MainMenuUi.SetActive(true);
        }

        if (GameStateManager.instance.MyGameState == GameStateManager.GameState.Pause)
        {
            PauseUi.SetActive(true);
        }
        
        SettingsUi.SetActive(false);
    }

    private void ShowWinScreen()
    {
        WinUi.SetActive(true);

        SettingsUi?.SetActive(false);
        MainMenuUi?.SetActive(false);
        PauseUi?.SetActive(false);
        WinUi.SetActive(false);
        LooseUi.SetActive(false);
    }

    private void ShowLooseScreen()
    {
        LooseUi.SetActive(true);

        SettingsUi?.SetActive(false);
        MainMenuUi?.SetActive(false);
        PauseUi?.SetActive(false);
        WinUi.SetActive(false);
        LooseUi.SetActive(false);
    }

    public void StartGame()
    {
        GameStateManager.instance.ChangeGameState?.Invoke(GameStateManager.GameState.Gameplay);
        SceneManager.LoadScene(1);

        
    }

    public void OpenLevelSelect()
    {
        MainMenuUi.SetActive(false);
        PauseUi.SetActive(false);
        LevelSelectMenu.SetActive(true);
    }

    public void CloseLevelSelect()
    {
        if (GameStateManager.instance.MyGameState == GameStateManager.GameState.MainMenu)
        {
            MainMenuUi.SetActive(true);
        }

        if (GameStateManager.instance.MyGameState == GameStateManager.GameState.Pause)
        {
            PauseUi.SetActive(true);
        }

        LevelSelectMenu.SetActive(false);
    }

   

    public void ResumeGame()
    {
        GameStateManager.instance.ChangeGameState?.Invoke(GameStateManager.GameState.Gameplay);
    }

    public void RestartLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);

        GameStateManager.instance.ChangeGameState?.Invoke(GameStateManager.GameState.Gameplay);
    }

    public void OpenMainMenu()
    {
        GameStateManager.instance.ChangeGameState?.Invoke(GameStateManager.GameState.MainMenu);

        
    }

    private void MainMenu()
    {
        MainMenuUi.SetActive(true);
        PauseUi.SetActive(false);
        GameplayUi.SetActive(false);
        SettingsUi.SetActive(false);
        LevelSelectMenu.SetActive(false);
    }

    public void LoadLevel(int buildIndex)
    {


        SceneManager.LoadScene(buildIndex);

        GameStateManager.instance.ChangeGameState?.Invoke(GameStateManager.GameState.Gameplay);
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
