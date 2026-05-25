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
    //[SerializeField] private float dispawnHeight = 70f;
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
        //_pooler=gameObject.GetComponent<ChunkPooler>();
        _pooler = levelChunkPoolers[currentLevelIndex];

        chunk = _pooler.GetChunk(); //Lo activa antes de usarlo
        chunk.transform.position = spawnPosition;
        chunk.GetComponent<TransformMovement>()?.SetSpeed(chunkSpeed);

        originalChunkSpeed = chunkSpeed; 
    }

    void Update()
    {
        if(chunk == null)
        {
            return;
        }
        if (chunk.transform.position.y >= spawnHeight/* && newChunk==null*/ && canSpawn)
        {           
            SpawnChunk();
        }
        //if(chunk.transform.position.y >= dispawnHeight)
        //{
        //    chunk.SetActive(false);
        //    chunk = newChunk;
        //    newChunk = null;
        //}
    }

    private void SpawnChunk()
    {
        if (chunksCount >= chunkThreshold)
        {
            chunksCount = 0;           
            //TransitionToNextLevel();
            return;
        }

        chunk = _pooler.GetChunk(); //Lo activa          
        chunk.transform.position = spawnPosition;
        chunk.GetComponent<TransformMovement>()?.SetSpeed(chunkSpeed);

        chunksCount++;
       
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

    public void OnWallDetection(Component sender, object data)
    {
        if(data is bool isWallDetected)
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
}
