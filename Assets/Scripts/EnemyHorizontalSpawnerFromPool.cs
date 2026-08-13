using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHorizontalSpawnerFromPool : MonoBehaviour
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

    [Header("Sincronización de nivel")]
    [Tooltip("Índices de nivel (según ChunkManager.levels) en los que este enemigo debe spawnear. Vacío = spawnea en todos los niveles.")]
    [SerializeField] private List<int> activeLevelIndices = new List<int>();

    private bool canSpawn = true;

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

            if (!canSpawn)
                continue;

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

            var tm = pooledObject.GetComponent<TransformMovement>();
            if (tm != null)
            {
                tm.SetHorizontalDirection(spawnLeft ? 1 : -1);
            }
        }
    }

    /// <summary>
    /// Handler para GameEventListener: activa/desactiva el spawn de este enemigo
    /// según si el nuevo nivel notificado por el ChunkManager master está en su lista de niveles activos.
    /// </summary>
    public void OnLevelChanged(Component sender, object data)
    {
        if (data is not int newLevelIndex)
            return;

        canSpawn = activeLevelIndices.Count == 0 || activeLevelIndices.Contains(newLevelIndex);
    }
}
