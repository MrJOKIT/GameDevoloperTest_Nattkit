using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public GameObject currentEnemy;

    private void OnEnable()
    {
        TimeHopManager.OnTimeChanged += OnTimeChanged;
    }

    private void OnDisable()
    {
        TimeHopManager.OnTimeChanged -= OnTimeChanged;
    }

    private void OnTimeChanged(TimePeriod time, int dayCount, WeekDay weekDay)
    {
        SpawnEnemy();
    }
    
    private void SpawnEnemy()
    {
        if (enemyPrefab != null && spawnPoint != null)
        {
            if (currentEnemy != null)
            {
                Destroy(currentEnemy);
            }
            currentEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
    
}
