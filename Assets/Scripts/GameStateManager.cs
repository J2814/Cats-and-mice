using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;

    public float CurretPlayTimeScale = 1;

    //public float 
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

    public void TimeScaleToggle()
    {
        if (Time.timeScale == 0) return;

        CurretPlayTimeScale += 0.5f;

        if (CurretPlayTimeScale > 2f)
        {
            CurretPlayTimeScale = 1;
        }

        Time.timeScale = CurretPlayTimeScale;
    }

    private void TimeScaleChangeBy(float amount)
    {
        if (Time.timeScale == 0) return;

        CurretPlayTimeScale += amount;

        if (CurretPlayTimeScale > 2f)
        {
            CurretPlayTimeScale = 2;
        }

        if (CurretPlayTimeScale < 1f)
        {
            CurretPlayTimeScale = 1;
        }

        Time.timeScale = CurretPlayTimeScale;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            TimeScaleChangeBy(-0.5f);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            TimeScaleChangeBy(0.5f);
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
                    Time.timeScale = CurretPlayTimeScale;
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
