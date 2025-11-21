using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetChunkFromPool : MonoBehaviour
{
    private Pooler _pooler;
    public GameObject chunk;
    public GameObject newChunk;
    void Start()
    {
        _pooler=gameObject.GetComponent<Pooler>();
        chunk = _pooler.GetPooledObject(); //Lo activa
        chunk.transform.position = Vector3.zero;
    }

    void Update()
    {
        if(chunk.transform.position.y >= 50 && newChunk==null)
        {           
            SpawnChunk();
        }
        if(chunk.transform.position.y >= 70)
        {
            chunk.SetActive(false);
            chunk = newChunk;
            newChunk = null;
        }
    }

    private void SpawnChunk()
    {
        newChunk = _pooler.GetRandomPooledObject(); //Lo activa             
        newChunk.transform.position = Vector3.zero;
    }
}
