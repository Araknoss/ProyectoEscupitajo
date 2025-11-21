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
    void Start()
    {
        _pooler=gameObject.GetComponent<ChunkPooler>();
        chunk = _pooler.GetPooledObject(); //Lo activa antes de usarlo
        chunk.transform.position = spawnPosition;
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
    }
}
