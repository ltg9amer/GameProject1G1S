using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private StageData stageData;
    [SerializeField] private ObjectPooler enemyPooler;
    [SerializeField] private float spawnDelay;
    private Vector2 spawnPosition;
    private bool spawnPositionSelector;

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            spawnPositionSelector = Random.value >= 0.5;

            if (spawnPositionSelector)
            {
                spawnPosition.x = (Random.value >= 0.5) ? stageData.LimitMax.x : stageData.LimitMin.x;
                spawnPosition.y = Random.Range((float)stageData.LimitMax.y, (float)stageData.LimitMin.y);
            }
            else if (!spawnPositionSelector)
            {
                spawnPosition.x = Random.Range((float)stageData.LimitMax.x, (float)stageData.LimitMin.x);
                spawnPosition.y = (Random.value >= 0.5) ? stageData.LimitMax.y : stageData.LimitMin.y;
            }

            enemyPooler.SpawnObject(spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
