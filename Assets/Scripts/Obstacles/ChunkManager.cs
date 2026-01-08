using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    private ChunkPooler _pooler;    
    private GameObject chunk;
    private GameObject newChunk;
    [SerializeField] private Vector3 spawnPosition;
    [SerializeField] private float spawnHeight = 50f;
    [SerializeField] private float dispawnHeight = 70f;
    [SerializeField] private float chunkSpeed;

    [Header("Level Transitions")]
    [SerializeField] private List<ChunkPooler> levelChunkPoolers;
    [SerializeField] private int chunkThreshold = 5;
    public int chunksCount = 0;
    public int currentLevelIndex = 0;

    void Start()
    {
        //_pooler=gameObject.GetComponent<ChunkPooler>();
        _pooler = levelChunkPoolers[currentLevelIndex];

        chunk = _pooler.GetPooledObject(); //Lo activa antes de usarlo
        chunk.transform.position = spawnPosition;
        chunk.GetComponent<TransformMovement>()?.SetSpeed(chunkSpeed);
    }

    void Update()
    {
        if(chunk.transform.position.y >= spawnHeight && newChunk==null)
        {           
            SpawnChunk();
        }
        if(chunk.transform.position.y >= dispawnHeight)
        {
            chunk.SetActive(false);
            chunk = newChunk;
            newChunk = null;
        }
    }

    private void SpawnChunk()
    {
        newChunk = _pooler.GetRandomPooledObject(); //Lo activa          
        newChunk.transform.position = spawnPosition;
        newChunk.GetComponent<TransformMovement>()?.SetSpeed(chunkSpeed);

        chunksCount++;
        if(chunksCount >= chunkThreshold)
        {
            chunksCount = 0;
            TransitionToNextLevel();
        }
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

    private void TransitionToNextLevel()
    {
        currentLevelIndex = (currentLevelIndex + 1) % levelChunkPoolers.Count;
        _pooler = levelChunkPoolers[currentLevelIndex];
    }
}
