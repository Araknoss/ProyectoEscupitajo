using System.Collections;
using UnityEngine;

public class GetEnemyFromPool : MonoBehaviour
{
    [SerializeField] private Pooler _pooler;

    [Header("Spawn points")]
    [SerializeField] private Transform leftSpawnPoint;
    [SerializeField] private Transform rightSpawnPoint;

    [Header("Spawn timing (s)")]
    [SerializeField] private float minInterval = 3f;
    [SerializeField] private float maxInterval = 10f;

    [Header("Height offset relative to spawn points")]
    [Tooltip("Offset aplicado a la posición Y del punto de spawn (puede ser negativo).")]
    [SerializeField] private float minHeightOffset = -1f;
    [SerializeField] private float maxHeightOffset = 1f;

    //[Header("Movement for TransformMovement")]
    //[Tooltip("Velocidad que se asignará a TransformMovement si está presente.")]
    //[SerializeField] private float movementSpeed = 2f;

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

            bool spawnLeft = Random.value < 0.5f;
            Transform basePoint = spawnLeft ? leftSpawnPoint : rightSpawnPoint;

            Vector3 spawnPos = (basePoint != null) ? basePoint.position : transform.position;
            float heightOffset = Random.Range(minHeightOffset, maxHeightOffset);
            spawnPos.y += heightOffset;

            pooledObject.transform.position = spawnPos;
            pooledObject.transform.localScale = spawnLeft ? Vector3.one : new Vector3(-1f, 1f, 1f);
            pooledObject.SetActive(true);

            var enemyMoveState = pooledObject.GetComponentInChildren<EnemyMoveState>();
            if (enemyMoveState != null)
            {
                enemyMoveState.SetHorizontalDirection(spawnLeft ? 1 : -1);                
            }

        }
    }
}
