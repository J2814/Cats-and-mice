using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour, IGameStateResonder
{


    //public static UiManager instance;

    public GameObject PauseUi;
    public GameObject GameplayUi;
    public GameObject WinUi;
    public GameObject LooseUi;


    public GameObject MainMenuUi;
    public GameObject LevelSelectMenu;

    bool paused = false;
    //public GameObject optionPanel;
    private void OnEnable()
    {
        GameStateManager.CurrentGameState += RespondToGameState;
    }

    private void OnDisable()
    {
        GameStateManager.CurrentGameState -= RespondToGameState;
    }

    //private void Awake()
    //{
    //    if (instance != null)
    //    {
    //        Destroy(gameObject);
    //    }
    //    else
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //}

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    Debug.Log(paused);
        //    if (paused)
        //    {
        //        ResumeGame();
        //        paused = false;
        //    }
        //    else
        //    {
        //        Pause();
        //        paused = true;

        //    }
            

            
        //}
    }

    public void RespondToGameState(GameStateManager.GameState gameState)
    {

        Debug.Log(gameState.ToString());
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

        
        //MainMenuUi?.SetActive(false);
        WinUi.SetActive(false);
        LooseUi.SetActive(false);

        AudioManager.instance.PlaySound(AudioManager.instance.SoundBank.Pause);

    }
    public void Gameplay()
    {
        GameplayUi.SetActive(true);

        AudioManager.instance.PlaySound(AudioManager.instance.SoundBank.Resume);
        //MainMenuUi?.SetActive(false);
        PauseUi?.SetActive(false);
        WinUi.SetActive(false);
        LooseUi.SetActive(false);
    }
    
    private void ShowWinScreen()
    {
        WinUi.SetActive(true);

        
        //MainMenuUi?.SetActive(false);
        PauseUi?.SetActive(false);
        //WinUi.SetActive(false);
        LooseUi.SetActive(false);
        AudioManager.instance.PlaySound(AudioManager.instance.SoundBank.Win);
    }

    private void ShowLooseScreen()
    {
        Debug.Log("ShowLooseScreen");
        LooseUi.SetActive(true);

        
        //MainMenuUi?.SetActive(false);
        PauseUi?.SetActive(false);
        WinUi.SetActive(false);
        //LooseUi.SetActive(false);
        AudioManager.instance.PlaySound(AudioManager.instance.SoundBank.Loose);
    }

    public void StartGame()
    {
        GameStateManager.instance.ChangeGameState?.Invoke(GameStateManager.GameState.Gameplay);
        SceneManager.LoadScene(1);
        AudioManager.instance.PlaySound(AudioManager.instance.SoundBank.GenericUi);


    }

    public void OpenLevelSelect()
    {
        MainMenuUi.SetActive(false);
        PauseUi.SetActive(false);
        LevelSelectMenu.SetActive(true);
        AudioManager.instance.PlaySound(AudioManager.instance.SoundBank.GenericUi);
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

        AudioManager.instance.PlaySound(AudioManager.instance.SoundBank.Pause);
    }

   

    public void ResumeGame()
    {
        GameStateManager.instance.ChangeGameState?.Invoke(GameStateManager.GameState.Gameplay);
        //AudioManager.instance.PlaySound(AudioManager.instance.SoundBank.GenericUi);
    }

    public void RestartLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);

        GameStateManager.instance.ChangeGameState?.Invoke(GameStateManager.GameState.Gameplay);
        AudioManager.instance.PlaySound(AudioManager.instance.SoundBank.Pause);
    }

    public void OpenMainMenu()
    {
        AudioManager.instance.PlaySound(AudioManager.instance.SoundBank.Pause);
        GameStateManager.instance.ChangeGameState?.Invoke(GameStateManager.GameState.MainMenu);

        
    }

    private void MainMenu()
    {
        //MainMenuUi.SetActive(true);
        //PauseUi.SetActive(false);
        GameplayUi.SetActive(false);

        //LevelSelectMenu.SetActive(false);
        SceneManager.LoadScene("StartMenu");
    }

    public void TimeScaleButtonToggle()
    {
        GameStateManager.instance.TimeScaleToggle();
    }

    public void LoadLevel(int buildIndex)
    {

        SceneManager.LoadScene(buildIndex);

        GameStateManager.instance.ChangeGameState?.Invoke(GameStateManager.GameState.Gameplay);
    }

    public void LoadNextLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        int buildIndex = currentScene.buildIndex;

        if (buildIndex >= SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(0);
            GameStateManager.instance.ChangeGameState?.Invoke(GameStateManager.GameState.MainMenu);
        }
        else
        {
            SceneManager.LoadScene(buildIndex + 1);
            GameStateManager.instance.ChangeGameState?.Invoke(GameStateManager.GameState.Gameplay);
        }
    }

    public void QuitGame()
    {
        AudioManager.instance.PlaySound(AudioManager.instance.SoundBank.Pause);
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
