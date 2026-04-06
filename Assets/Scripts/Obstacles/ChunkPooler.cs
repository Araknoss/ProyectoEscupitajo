using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkPooler : MonoBehaviour
{   
    [SerializeField] private List<GameObject> poolList = new List<GameObject>();
    [SerializeField] private int poolSize = 5;

    public GameObject lastChunk;
    void Awake()
    {
        CreatePool();        
    }

    private void CreatePool()
    {
        for(int i = 0; i < poolSize; i++)
        {            
            GameObject obj = gameObject.transform.GetChild(i).gameObject;
            obj.SetActive(false);
            poolList.Add(obj);
        }
    }

    public GameObject GetPooledObject() //Devuelve el primer objeto inactivo del pool y lo activa
    {
        for(int i = 0; i < poolList.Count; i++)
        {
            if (!poolList[i].activeInHierarchy)
            {
                poolList[i].SetActive(true);
                return poolList[i];
            }
        }
        return null;
    }

    public GameObject GetRandomPooledObject() //Busca un objeto aleatorio del pool que este inactivo y lo activa
    {
        for (int i = 0; i < poolList.Count; i++)
        {
            int randomIndex = Random.Range(0, poolList.Count);
            if (!poolList[randomIndex].activeInHierarchy)
            {
                poolList[randomIndex].SetActive(true);
                return poolList[randomIndex];
            }
        }
        return null;
    }

    public GameObject GetLastChunk()
    {
        lastChunk.SetActive(true);
        return lastChunk;
    }
}
