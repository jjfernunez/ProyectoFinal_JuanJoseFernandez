using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject [] prefabToSpawn;

    public Transform playerPos;
    public float spawnDistanceMin = -10f;
    public float spawnDistanceMax = 10f;
    public float timeBetweenSpawns = 100;
    public float spawnRateIncreaseTime = 10;
    public float spawnRateDecrease = 10;
    public int spawnAmount = 0;
    Vector2 spawnPosition;

    void Start()
    {
        StartCoroutine(SpawnTimer());
        
    }

    private void Update()
    {
        
    }

    IEnumerator IncreaseSpawnRate()
    {
        yield return new WaitForSeconds(spawnRateIncreaseTime);
        timeBetweenSpawns -= spawnRateDecrease;
        Debug.Log("Decrease");
    }
    IEnumerator SpawnTimer()
    {
        while (true)
        {
            //yield return StartCoroutine(IncreaseSpawnRate());
            for (int i = 0; i < spawnAmount; i++)
            {
                spawnPosition = new Vector2(playerPos.position.x + Random.Range(spawnDistanceMin, spawnDistanceMax), playerPos.position.y + Random.Range(spawnDistanceMin, spawnDistanceMax));
                Instantiate(prefabToSpawn[Random.Range(0,3)], spawnPosition, Quaternion.identity);
            }
            yield return new WaitForSeconds(timeBetweenSpawns);
            
        }
    }
}
