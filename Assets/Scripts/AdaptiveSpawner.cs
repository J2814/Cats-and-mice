using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdaptiveSpawner : Spawner
{
    private LevelManager levelManager;

    public float SpawnTime;
    private float currentSpawnTime;

    [SerializeField]
    private int MaxSpawnedCats;
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

   
}
