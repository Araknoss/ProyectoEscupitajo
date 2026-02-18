using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetVerticalEnemyFromPool : MonoBehaviour
{
    [SerializeField] private Pooler _pooler;

    [Header("Spawn points")]
    [SerializeField] private Transform leftSpawnPoint;
    [SerializeField] private Transform rightSpawnPoint;

    [Header("Spawn timing (s)")]
    [SerializeField] private float minInterval = 3f;
    [SerializeField] private float maxInterval = 10f;    

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

            float spawnPosOnX= Random.Range(leftSpawnPoint.position.x, rightSpawnPoint.position.x);
            Vector3 spawnPos = new Vector3(spawnPosOnX, leftSpawnPoint.position.y, leftSpawnPoint.position.z);

            pooledObject.transform.position = spawnPos;           
            pooledObject.SetActive(true);
        }
    }
}
