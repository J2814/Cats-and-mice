using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public bool KillCatsToWin;

    [Header("Dead Cats Win Condition")]
    public int NumberOfDeadCatsToWin;
    public int CurrentNumberOfDeadCats;

    [Header("Saved Mice Win Condition")]
    public int NumberOfSavedMiceToWin;
    public int CurrentNumberOfSavedMice;

    [Header("~~~~~")]
    public int NumberOfDeadMiceToLoose;
    public int CurrentNumberOfDeadMice;

    public static Action CatDied;
    public static Action MouseDied;
    public static Action CatSpawned;
    public static Action MouseSpawned;
    public static Action MouseSaved;

    public static Action Pause;
    public static Action EndLevel;

    public static Action<int, int> CurrentScore;

    public int CurrentSpawnedCats;
    public int CurrentSpawnedMice;


    private bool LevelEnded = false;


   

    private void OnEnable()
    {
        CatDied += UpdateDeadCats;
        MouseDied += UpdateDeadMice;
        CatSpawned += UpdateSpawnedCats;
        MouseSpawned += UpdateSpawnedMice;
        MouseSaved += UpdateSavedMice;
        Pause += PauseGame;
    }

    private void OnDisable()
    {
        CatDied -= UpdateDeadCats;
        MouseDied -= UpdateDeadMice;
        CatSpawned -= UpdateSpawnedCats;
        MouseSpawned -= UpdateSpawnedMice;
        MouseSaved -= UpdateSavedMice;
        Pause -= PauseGame;
    }

    private void Start()
    {
        CurrentScore?.Invoke(CurrentNumberOfDeadCats, NumberOfDeadCatsToWin);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) PauseGame(); //Debug.Log("Pause");
    }
    private void UpdateDeadCats()
    {
        CurrentNumberOfDeadCats++;
        CurrentSpawnedCats--;

        if (KillCatsToWin)
        {
            CurrentScore?.Invoke(CurrentNumberOfDeadCats, NumberOfDeadCatsToWin);
            if (CurrentNumberOfDeadCats >= NumberOfDeadCatsToWin)
            {
                Debug.Log("Win");
                GameStateManager.instance.ChangeGameState?.Invoke(GameStateManager.GameState.WinScreen);
                //UiManager.WinScreen?.Invoke();
                EndLevel?.Invoke();
            }
        }
    }

    private void UpdateSavedMice()
    {
        CurrentNumberOfSavedMice++;
        CurrentSpawnedMice--;

        if (!KillCatsToWin)
        {
            if (CurrentNumberOfSavedMice >= NumberOfSavedMiceToWin)
            {
                Debug.Log("Win");
                GameStateManager.instance.ChangeGameState?.Invoke(GameStateManager.GameState.WinScreen);
                //UiManager.WinScreen?.Invoke();
                EndLevel?.Invoke();
            }
        }
        
    }

    private void UpdateDeadMice()
    {
        CurrentNumberOfDeadMice++;
        CurrentSpawnedMice--;
        

        if (CurrentNumberOfDeadMice >= NumberOfDeadMiceToLoose)
        {
            Debug.Log("Loose");
            GameStateManager.instance.ChangeGameState?.Invoke(GameStateManager.GameState.LooseScreen);
            //UiManager.LooseScreen?.Invoke();
            EndLevel?.Invoke();
        }
    }

    private void UpdateSpawnedCats()
    {
        CurrentSpawnedCats++;
    }

    private void UpdateSpawnedMice()
    {
        CurrentSpawnedMice++;
    }


    private void PauseGame()
    {
        if (LevelEnded) return;

        GameStateManager.instance.ChangeGameState?.Invoke(GameStateManager.GameState.Pause);
    }
}
