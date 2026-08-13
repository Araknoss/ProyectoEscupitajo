using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVerticalSpawnerFromPool : MonoBehaviour
{
    [SerializeField] private Pooler _pooler;

    [Header("Spawn points")]
    [Tooltip("Puntos entre los que se elige una posición aleatoria para spawnear el enemigo.")]
    [SerializeField] private Transform leftSpawnPoint;
    [SerializeField] private Transform rightSpawnPoint;

    [Header("Spawn timing (s)")]
    [SerializeField] private float minInterval = 3f;
    [SerializeField] private float maxInterval = 10f;

    [Header("Movement")]
    [Tooltip("Dirección de movimiento del enemigo tras spawnear.")]
    [SerializeField] private Vector3 movementDirection = Vector3.down;

    [Header("Sincronización de nivel")]
    [Tooltip("Índices de nivel (según ChunkManager.levels) en los que este enemigo debe spawnear. Vacío = spawnea en todos los niveles.")]
    [SerializeField] private List<int> activeLevelIndices = new List<int>();

    private bool canSpawn = true;

    private void Start()
    {
        StartCoroutine(SpawnEnemiesRandomly());
    }

    private IEnumerator SpawnEnemiesRandomly()
    {
        while (true)
        {
            float waitTime = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(waitTime);

            if (!canSpawn)
                continue;

            GameObject pooledObject = _pooler?.GetPooledObject();
            if (pooledObject == null) continue;

            Vector3 spawnPos = GetRandomSpawnPosition();

            pooledObject.transform.position = spawnPos;
            pooledObject.SetActive(true);

            var tm = pooledObject.GetComponent<TransformMovement>();
            if (tm != null)
            {
                tm.SetDirection(movementDirection);
            }
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        if (leftSpawnPoint == null || rightSpawnPoint == null)
        {
            return transform.position;
        }

        float t = Random.value;
        return Vector3.Lerp(leftSpawnPoint.position, rightSpawnPoint.position, t);
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