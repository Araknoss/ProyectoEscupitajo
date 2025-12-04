using System.Collections;
using UnityEngine;

public class SpawnHorizontalEnemyFromPool : MonoBehaviour
{
    [SerializeField] private Pooler _pooler;

    [Header("Spawn points")]
    [SerializeField] private Transform leftSpawnPoint;
    [SerializeField] private Transform rightSpawnPoint;

    [Header("Spawn timing (s)")]
    [SerializeField] private float minInterval = 3f;
    [SerializeField] private float maxInterval = 10f;

    [Header("Offset")]    
    [SerializeField] private float minHeightOffset = -1f;
    [SerializeField] private float maxHeightOffset = 1f;

    private bool spawnLeft;    

    private void Start()
    {
        StartCoroutine(SpawnObstaclesRandomly());
    }

    private IEnumerator SpawnObstaclesRandomly()
    {
        while (true)
        {
            float waitTime = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(waitTime);

            GameObject pooledObject = _pooler?.GetPooledObject();
            if (pooledObject == null) continue;

            Transform basePoint = ChooseRandomSpawnPoint();
            SpawnWithOffset(pooledObject, basePoint);

            var enemyMoveState = pooledObject.GetComponentInChildren<EnemyMoveState>();
            if (enemyMoveState != null)
            {
                enemyMoveState.SetHorizontalDirection(spawnLeft ? 1 : -1);                
            }
        }
    }

    private Transform ChooseRandomSpawnPoint()
    {
        spawnLeft = Random.value < 0.5f;
        Transform basePoint = spawnLeft ? leftSpawnPoint : rightSpawnPoint;
        return basePoint;
    }

    private void SpawnWithOffset(GameObject pooledObject, Transform basePoint)
    {
        Vector3 spawnPos = (basePoint != null) ? basePoint.position : transform.position;
        float heightOffset = Random.Range(minHeightOffset, maxHeightOffset);
        spawnPos.y += heightOffset;

        pooledObject.transform.position = spawnPos;
        pooledObject.transform.localScale = spawnLeft ? Vector3.one : new Vector3(-1f, 1f, 1f);
        pooledObject.SetActive(true);
    }
}
