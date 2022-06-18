using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private StageData stageData;
    [SerializeField] private ObjectPooler enemyPooler;
    [SerializeField] private int maxSpawnEnemy;
    [SerializeField] private float spawnDelay;
    private Vector2 spawnPosition;
    private int currentSpawnEnemy;
    private bool spawnPositionSelector;

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private void Update()
    {
        if (currentSpawnEnemy == maxSpawnEnemy)
        {
            StopAllCoroutines();
        }
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            spawnPositionSelector = Random.value >= 0.5;

            if (spawnPositionSelector)
            {
                spawnPosition.x = (Random.value >= 0.5) ? stageData.LimitMax.x : stageData.LimitMin.x;
                spawnPosition.y = Random.Range(stageData.LimitMin.y, stageData.LimitMax.y);
            }
            else if (!spawnPositionSelector)
            {
                spawnPosition.x = Random.Range(stageData.LimitMin.x, stageData.LimitMax.x);
                spawnPosition.y = (Random.value >= 0.5) ? stageData.LimitMax.y : stageData.LimitMin.y;
            }

            enemyPooler.SpawnObject(spawnPosition, Quaternion.identity);
            currentSpawnEnemy++;

            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
