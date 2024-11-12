using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int NumberOfDeadCatsToWin;
    public int CurrentNumberOfDeadCats;

    public int NumberOfDeadMiceToLoose;
    public int CurrentNumberOfDeadMice;

    public static Action CatDied;
    public static Action MouseDied;
    public static Action CatSpawned;
    public static Action Pause;
    public static Action EndLevel;


    public int CurrentSpawnedCats;

    private bool isPaused = false;

    private bool LevelEnded = false;

    private void OnEnable()
    {
        CatDied += UpdateDeadCats;
        MouseDied += UpdateDeadMice;
        CatSpawned += UpdateSpawnedCats;
        Pause += PauseGame;
    }

    private void OnDisable()
    {
        CatDied -= UpdateDeadCats;
        MouseDied -= UpdateDeadMice;
        CatSpawned -= UpdateSpawnedCats;
        Pause -= PauseGame;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) PauseGame(); Debug.Log("Pause");
    }
    private void UpdateDeadCats()
    {
        CurrentNumberOfDeadCats++;
        CurrentSpawnedCats--;

        if (CurrentNumberOfDeadCats >= NumberOfDeadCatsToWin)
        {
            Debug.Log("Win");
            UiManager.WinScreen?.Invoke();
            EndLevel?.Invoke();
        }
    }

    private void UpdateDeadMice()
    {
        CurrentNumberOfDeadMice++;

        if (CurrentNumberOfDeadMice >= NumberOfDeadMiceToLoose)
        {
            Debug.Log("Loose");
            UiManager.LooseScreen?.Invoke();
            EndLevel?.Invoke();
        }
    }

    private void UpdateSpawnedCats()
    {
        CurrentSpawnedCats++;
    }


    private void PauseGame()
    {
        if (LevelEnded) return;

        isPaused = !isPaused;

        Time.timeScale = isPaused ? 0 : 1;


        UiManager.PauseAction?.Invoke(isPaused);
    }
}
