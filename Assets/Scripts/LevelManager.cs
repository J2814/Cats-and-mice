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
    public int CurrentSpawnedCats;

    private void OnEnable()
    {
        CatDied += UpdateDeadCats;
        MouseDied += UpdateDeadMice;
        CatSpawned += UpdateSpawnedCats;
    }

    private void OnDisable()
    {
        CatDied -= UpdateDeadCats;
        MouseDied -= UpdateDeadMice;
        CatSpawned -= UpdateSpawnedCats;
    }

    private void UpdateDeadCats()
    {
        CurrentNumberOfDeadCats++;
        CurrentSpawnedCats--;

        if (CurrentNumberOfDeadCats >= NumberOfDeadCatsToWin)
        {
            Debug.Log("Win");
        }
    }

    private void UpdateDeadMice()
    {
        CurrentNumberOfDeadMice++;

        if (CurrentNumberOfDeadMice >= NumberOfDeadMiceToLoose)
        {
            Debug.Log("Loose");
        }
    }

    private void UpdateSpawnedCats()
    {
        CurrentSpawnedCats++;
    }
}
