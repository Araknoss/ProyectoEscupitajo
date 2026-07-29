using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBuildingManager : MonoBehaviour
{
    private MainBuildingPooler _pooler;
    private GameObject chunk;
    private GameObject newChunk;
    [SerializeField] private Vector3 spawnPosition;
    [SerializeField] private float spawnHeight = 50f;
    [SerializeField] private float chunkSpeed;

    [Header("Level Transitions")]
    [SerializeField] private List<MainBuildingPooler> levelChunkPoolers;
    [SerializeField] private int chunkThreshold = 5;
    public int chunksCount = 0;
    public int currentLevelIndex = 0;

    [SerializeField] private float onWallSlowdownFactor = 0.8f;
    private float originalChunkSpeed;

    private bool canSpawn = true;
    public bool demoEnd = false;

    void Start()
    {
        InitializeCurrentPooler();

        chunk = _pooler.GetChunk(); //Lo activa antes de usarlo
        chunk.transform.position = spawnPosition;
        chunk.GetComponent<TransformMovement>()?.SetSpeed(chunkSpeed);

        originalChunkSpeed = chunkSpeed;
    }

    void Update()
    {
        if (chunk == null)
        {
            return;
        }
        if (chunk.transform.position.y >= spawnHeight && canSpawn)
        {
            SpawnChunk();
        }
    }

    private void SpawnChunk()
    {
        chunk = _pooler.GetChunk(); //Lo activa
        if (chunk == null)
        {
            return;
        }
        chunk.transform.position = spawnPosition;
        chunk.GetComponent<TransformMovement>()?.SetSpeed(chunkSpeed);

        chunksCount++;
    }

    private void InitializeCurrentPooler()
    {
        if (levelChunkPoolers == null || levelChunkPoolers.Count == 0)
        {
            Debug.LogError("MainBuildingManager no tiene MainBuildingPoolers asignados.");
            return;
        }

        if (currentLevelIndex < 0 || currentLevelIndex >= levelChunkPoolers.Count)
        {
            currentLevelIndex = 0;
        }

        _pooler = levelChunkPoolers[currentLevelIndex];
    }

    public void SetChunkSpeed(Component sender, object data)
    {
        Debug.Log("Chunk speed changed to: " + data);
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
        if (data is bool isWallDetected)
        {
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
    }

    /// <summary>
    /// Handler para GameEventListener: sincroniza este MainBuildingManager (esclavo)
    /// cuando el ChunkManager master notifica un cambio de nivel.
    /// </summary>
    public void OnLevelChanged(Component sender, object data)
    {
        if (data is not int newLevelIndex)
            return;

        if (levelChunkPoolers == null || newLevelIndex < 0 || newLevelIndex >= levelChunkPoolers.Count)
            return;

        currentLevelIndex = newLevelIndex;
        chunksCount = 0;

        InitializeCurrentPooler();
    }
}