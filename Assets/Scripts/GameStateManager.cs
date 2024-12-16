using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;

    public enum GameState
    {
        Gameplay,
        Pause,
        MainMenu,
        WinScreen,
        LooseScreen
    }

    public GameState MyGameState;

    public Action<GameState> ChangeGameState;

    public static Action<GameState> CurrentGameState;

    private void OnEnable()
    {
        ChangeGameState += GameStateChange;
    }

    private void OnDisable()
    {
        ChangeGameState -= GameStateChange;
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

    private void GameStateChange(GameState newGameState)
    {
        MyGameState = newGameState;
        TimeManagement();
        CurrentGameState?.Invoke(MyGameState);
    }

    private void TimeManagement()
    {
        switch (MyGameState)
        {
            case GameState.Pause:
                {
                    Time.timeScale = 0;

                    break;
                }
            case GameState.MainMenu:
                {
                    Time.timeScale = 0;
                    break;
                }
            case GameState.Gameplay:
                {
                    Time.timeScale = 1;
                    break;
                }
            case GameState.WinScreen:
                {
                    Time.timeScale = 0;
                    break;
                }
            case GameState.LooseScreen:
                {
                    Time.timeScale = 0;
                    break;
                }

            default: break;
        }
    }

}
