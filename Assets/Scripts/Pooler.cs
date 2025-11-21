using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    [SerializeField] private List<GameObject> pooledObjects = new List<GameObject>();
    //[SerializeField] private int poolAmount = 20;
    [SerializeField] private List<GameObject> poolList = new List<GameObject>();
    // Start is called before the first frame update
    void Awake()
    {
        CreatePool();
        
    }

    private void CreatePool()
    {
        for(int i = 0; i < pooledObjects.Count; i++)
        {            
            GameObject obj = Instantiate(pooledObjects[i],gameObject.transform);
            obj.SetActive(false);
            poolList.Add(obj);
        }
    }

    public GameObject GetPooledObject() //Devuelve el primer objeto inactivo del pool y lo activa
    {
        for(int i = 0; i < pooledObjects.Count; i++)
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
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            int randomIndex = Random.Range(0, pooledObjects.Count);
            if (!poolList[randomIndex].activeInHierarchy)
            {
                poolList[randomIndex].SetActive(true);
                return poolList[randomIndex];
            }
        }
        return null;
    }
}
