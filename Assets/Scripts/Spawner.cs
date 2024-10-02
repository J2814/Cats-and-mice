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
            SpawnUnit(GetRandomSpawnPoint());
        }
        
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

        Instantiate(UnitPrefab);

        
        UnitPrefab.GetComponent<Unit>().Transition(sp);
        
    }
}
