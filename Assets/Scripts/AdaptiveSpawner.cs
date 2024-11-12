using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdaptiveSpawner : Spawner
{
    private LevelManager levelManager;

    public float SpawnTime;
    private float currentSpawnTime;

    private bool AllowSpawn = true;

    [SerializeField]
    private int MaxSpawnedCats;

    private void OnEnable()
    {
        LevelManager.EndLevel += StopSpawning;
    }

    private void OnDisable()
    {
        LevelManager.EndLevel -= StopSpawning;
    }
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        TimedRandomSpawn();
    }

    private void TimedRandomSpawn()
    {
        if (!AllowSpawn) return;

        currentSpawnTime -= Time.deltaTime;
        if (currentSpawnTime <= 0)
        {
            if (levelManager.CurrentSpawnedCats < MaxSpawnedCats)
            {
                RandomSpawn();
            }
            currentSpawnTime = SpawnTime;
        }
    }

    private void StopSpawning()
    {
        AllowSpawn = false;
    }
   
}
