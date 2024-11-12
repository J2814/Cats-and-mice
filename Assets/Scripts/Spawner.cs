using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private Transform[] SpawnPoints;
    [SerializeField]
    private GameObject UnitPrefab;



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            RandomSpawn();
        }

    }

    internal void RandomSpawn()
    {
        SpawnUnit(GetRandomSpawnPoint());
    }

    private Transform GetRandomSpawnPoint()
    {
        if (SpawnPoints == null)
        {
            return null;
        }

        int index = Random.Range(0, SpawnPoints.Length);

        return SpawnPoints[index];
    }

    private void SpawnUnit(Transform sp)
    {
        if (sp == null)
        {
            Debug.Log("Spawner assigned null spawn point");
            return;
        }

        GameObject unit = Instantiate(UnitPrefab);

        if (unit.CompareTag("Cat"))
        {
            LevelManager.CatSpawned?.Invoke();
        }

        if (unit.CompareTag("Mouse"))
        {
            LevelManager.MouseSpawned?.Invoke();
        }

        if (unit.GetComponent<MovementController>() != null)
        {
            unit.GetComponent<MovementController>().Transition(sp);
            unit.transform.position = sp.transform.position;
        }
    }
}
