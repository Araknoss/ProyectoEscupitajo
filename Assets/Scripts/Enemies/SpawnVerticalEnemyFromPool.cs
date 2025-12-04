using System.Collections;
using UnityEngine;

public class SpawnVerticalEnemyFromPool : MonoBehaviour
{
    [SerializeField] private Pooler _pooler;

    [Header("Spawn points")]
    [SerializeField] private Transform spawnPoint;    

    [Header("Spawn timing (s)")]
    [SerializeField] private float minInterval = 3f;
    [SerializeField] private float maxInterval = 10f;

    [Header("Offset")]    
    [SerializeField] private float minWidthtOffset = -1f;
    [SerializeField] private float maxWidthOffset = 1f;

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
            
            SpawnWithOffset(pooledObject, spawnPoint);            
        }
    }

    private void SpawnWithOffset(GameObject pooledObject, Transform basePoint)
    {
        Vector3 spawnPos = (basePoint != null) ? basePoint.position : transform.position;
        float widthOffset = Random.Range(minWidthtOffset, maxWidthOffset);
        spawnPos.x += widthOffset;

        pooledObject.transform.position = spawnPos;       
        pooledObject.SetActive(true);
    }
}
