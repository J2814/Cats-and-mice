using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class AdaptiveSpawner : Spawner
{
    public bool CatSpawner;

    private LevelManager levelManager;

    public float SpawnTime;
    private float currentSpawnTime;

    public enum StartUpDelaySpawn
    {
        TimedDelay,
        KeyPressDelay
    }

    public StartUpDelaySpawn StartUpType = StartUpDelaySpawn.TimedDelay;

    public KeyCode SpawnKey;

    public int SpawnKeyPressesRequired = 3;

    private int spawnKeyPresses;


    public float StartUpDelayTime = 1;


    private bool AllowSpawn = false;

    [SerializeField]
    private int MaxSpawnedUnits;

    private void OnEnable()
    {
        LevelManager.EndLevel += StopSpawning;
        LevelManager.PreEndLevel += StopSpawning;
    }

    private void OnDisable()
    {
        LevelManager.EndLevel -= StopSpawning;
        LevelManager.PreEndLevel -= StopSpawning;
    }
    void Start()
    {
        if (StartUpType == StartUpDelaySpawn.TimedDelay)
        {
            StartCoroutine(StartUpDelay());
        }

        levelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        KeyPressesDelay();

        TimedRandomSpawn();
    }

    private void KeyPressesDelay()
    {
        if (StartUpType == StartUpDelaySpawn.KeyPressDelay)
        {
            if (Input.GetKeyDown(SpawnKey))
            {
                spawnKeyPresses++;
            }

            if (spawnKeyPresses >= SpawnKeyPressesRequired)
            {
                AllowSpawn = true;
            }
        }
    }

    private void TimedRandomSpawn()
    {
        if (!AllowSpawn) return;

        currentSpawnTime -= Time.deltaTime;
        if (currentSpawnTime <= 0)
        {
            if (CatSpawner)
            {
                if (levelManager.CurrentSpawnedCats < MaxSpawnedUnits)
                {
                    RandomSpawn();
                    AudioManager.instance.PlaySound(AudioManager.instance.SoundBank.CatSpawn);
                }
            }
            else
            {
                if (levelManager.CurrentSpawnedMice < MaxSpawnedUnits)
                {
                    RandomSpawn();
                    AudioManager.instance.PlaySound(AudioManager.instance.SoundBank.MouseSpawn);
                }
            }

            currentSpawnTime = SpawnTime;
        }
    }

    private void StopSpawning()
    {
        AllowSpawn = false;
    }
   


    public IEnumerator StartUpDelay()
    {
        yield return new WaitForSeconds(StartUpDelayTime);

        AllowSpawn = true;
    }
}
