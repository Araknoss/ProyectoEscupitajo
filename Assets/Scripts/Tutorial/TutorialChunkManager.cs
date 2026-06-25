using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialChunkManager : MonoBehaviour
{
    [SerializeField] private ChunkPooler _pooler;
   private GameObject chunk;
    private GameObject newChunk;

    [Header("Spawn")]
    [SerializeField] private Vector3 spawnPosition;
    [SerializeField] private float spawnHeight = 50f;
    [SerializeField] private float chunkSpeed;

    public int chunksCount = 0;  

    private void Start()
    {
        SpawnChunk();      
    }

    private void Update()
    {
        if (chunk.transform.position.y >= spawnHeight)
        {
            SpawnChunk();
        }
    }

    private void SpawnChunk()
    {
        if (chunksCount == 0)
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
}
