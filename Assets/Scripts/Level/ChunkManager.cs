using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    private ChunkPooler _pooler;
    private GameObject chunk;
    private GameObject newChunk;

    [Header("Spawn")]
    [SerializeField] private Vector3 spawnPosition;
    [SerializeField] private float spawnHeight = 50f;
    [SerializeField] private float chunkSpeed;

    [Header("Levels")]
    [SerializeField] private List<LevelData> levels;
    [SerializeField] private LevelData tutorialLevel;

    [Tooltip("Chunks totales antes de pasar al siguiente nivel")]
    [SerializeField] private int chunkThreshold = 12;

    [Tooltip("Chunks antes de pasar al siguiente pooler dentro del nivel")]
    [SerializeField] private int chunksPerPooler = 3;

    public int chunksCount = 0;
    public int currentLevelIndex = 0;

    private int currentPoolerIndex = 0;
    private int chunksInCurrentPooler = 0;

    [Header("Wall Detection")]
    [SerializeField] private float onWallSlowdownFactor = 0.8f;

    private float originalChunkSpeed;

    private bool canSpawn = true;
    public bool demoEnd = false;

    [SerializeField] private bool addRandomness = false;
    [SerializeField] private float heightSpawnRandomness = 30f;
    [SerializeField] private float randomSpawnHeight;

    [Header("Sincronización de nivel")]
    [Tooltip("Si es true, este ChunkManager decide el avance de nivel y notifica al resto")]
    [SerializeField] private bool isMaster = true;
    [SerializeField] private GameEvent onLevelChanged;

    [Header("Chunk Especial (Foreground)")]
    [Tooltip("Chunk especial a spawnear una única vez en la transición indicada")]
    [SerializeField] private GameObject specialChunk;
    [SerializeField] private int specialChunkFromLevelIndex = 0;
    [SerializeField] private int specialChunkToLevelIndex = 1;
    private bool specialChunkUsed = false;
    private bool pendingSpecialChunk = false;

    private void Start()
    {
        InitializeCurrentPooler();

        SpawnChunk();

        originalChunkSpeed = chunkSpeed;

        randomSpawnHeight = spawnHeight;
    }

    private void Update()
    {
        if (chunk != null &&
            chunk.transform.position.y >= randomSpawnHeight &&
            canSpawn)
        {
            SpawnChunk();
        }
    }

    private void SpawnChunk()
    {
        if (chunksCount >= chunkThreshold)
        {
            chunksCount = 0;

            if (demoEnd)
            {
                DemoEnd();
                return;
            }

            if (isMaster)
            {
                TransitionToNextLevel();
                onLevelChanged?.Raise(this, currentLevelIndex);
            }

            return;
        }

        CheckPoolerTransition();

        LevelData currentLevel = levels[currentLevelIndex];

        if (pendingSpecialChunk && specialChunk != null)
        {
            chunk = specialChunk;
            chunk.SetActive(true);

            pendingSpecialChunk = false;
            specialChunkUsed = true;
        }
        else if (chunksCount == 0 && currentLevel.firstChunk != null)
        {
            chunk = currentLevel.firstChunk;
            chunk.SetActive(true);
        }
        else if (chunksCount == chunkThreshold - 1 && currentLevel.lastChunk != null)
        {
            chunk = currentLevel.lastChunk;
            chunk.SetActive(true);
        }
        else if (chunksCount == 0)
        {
            chunk = _pooler.GetFirstChunk();
        }
        else
        {
            chunk = _pooler.GetRandomPooledObject();
        }

        chunk.transform.position = spawnPosition;
        chunk.GetComponent<TransformMovement>()?.SetSpeed(chunkSpeed);

        chunksCount++;
        chunksInCurrentPooler++;

        if (addRandomness)
        {
            randomSpawnHeight = Random.Range(spawnHeight - heightSpawnRandomness, spawnHeight + heightSpawnRandomness);
        }
    }

    private void CheckPoolerTransition()
    {
        LevelData currentLevel = levels[currentLevelIndex];

        if (currentLevel.chunkPoolers.Count <= 1)
            return;

        if (chunksInCurrentPooler < chunksPerPooler)
            return;

        chunksInCurrentPooler = 0;

        currentPoolerIndex++;

        if (currentPoolerIndex >= currentLevel.chunkPoolers.Count)
        {
            currentPoolerIndex = currentLevel.chunkPoolers.Count - 1;
        }

        _pooler = currentLevel.chunkPoolers[currentPoolerIndex];
        if (_pooler.obstaclePooler != null)
        {
            _pooler.obstaclePooler.SetActive(true);
        }
    }

    private void TransitionToNextLevel()
    {
        currentLevelIndex++;

        if (currentLevelIndex >= levels.Count)
        {
            currentLevelIndex = 0;
        }

        currentPoolerIndex = 0;
        chunksInCurrentPooler = 0;

        InitializeCurrentPooler();
    }

    private void InitializeCurrentPooler()
    {

        LevelData currentLevel = levels[currentLevelIndex];

        if (currentLevel.chunkPoolers.Count == 0)
        {
            Debug.LogError($"El nivel {currentLevel.levelName} no tiene ChunkPoolers asignados.");
            return;
        }

        chunkThreshold = (int)currentLevel.chunkTreshold;
        chunksPerPooler = (int)currentLevel.chunksPerPooler;
        _pooler = currentLevel.chunkPoolers[currentPoolerIndex];
    }

    private void DemoEnd()
    {
        canSpawn = false;

        chunk = _pooler.GetLastChunk();

        chunk.transform.position = spawnPosition;
        chunk.GetComponent<TransformMovement>()?.SetSpeed(chunkSpeed);
    }

    public void SetChunkSpeed(Component sender, object data)
    {
        chunkSpeed = (float)data;

        if (chunk != null)
        {
            chunk.GetComponent<TransformMovement>()?.SetSpeed(chunkSpeed);
        }

        if (newChunk != null)
        {
            newChunk.GetComponent<TransformMovement>()?.SetSpeed(chunkSpeed);
        }
    }

    public void OnWallDetection(Component sender, object data)
    {
        if (data is not bool isWallDetected)
            return;

        if (isWallDetected)
        {
            chunkSpeed *= onWallSlowdownFactor;
        }
        else
        {
            chunkSpeed = originalChunkSpeed;
        }

        if (chunk != null)
        {
            chunk.GetComponent<TransformMovement>()?.SetSpeed(chunkSpeed);
        }

        if (newChunk != null)
        {
            newChunk.GetComponent<TransformMovement>()?.SetSpeed(chunkSpeed);
        }
    }

    /// <summary>
    /// Handler para GameEventListener: sincroniza este ChunkManager (esclavo)
    /// cuando el ChunkManager master notifica un cambio de nivel.
    /// </summary>
    public void OnLevelChanged(Component sender, object data)
    {
        if (isMaster)
            return;

        if (data is not int newLevelIndex)
            return;

        if (!specialChunkUsed &&
            currentLevelIndex == specialChunkFromLevelIndex &&
            newLevelIndex == specialChunkToLevelIndex)
        {
            pendingSpecialChunk = true;
        }

        currentLevelIndex = newLevelIndex;
        currentPoolerIndex = 0;
        chunksInCurrentPooler = 0;
        chunksCount = 0;

        InitializeCurrentPooler();
    }
}
