using System.Collections;
using System.Collections.Generic;
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

    [Header("Sincronización de nivel")]
    [Tooltip("Índices de nivel (según ChunkManager.levels) en los que este enemigo debe spawnear. Vacío = spawnea en todos los niveles.")]
    [SerializeField] private List<int> activeLevelIndices = new List<int>();

    private bool spawnLeft;
    private bool canSpawn = false;

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
