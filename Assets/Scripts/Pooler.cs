using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    [SerializeField] private GameObject pooledObject;
    [SerializeField] private int poolAmount = 20;
    [SerializeField] private List<GameObject> poolList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < poolAmount; i++)
        {
            GameObject obj = Instantiate(pooledObject,gameObject.transform);
            poolList.Add(obj);
            obj.SetActive(false); //obvejota
        }
    }

    public GameObject GetPooledObject()
    {
        for(int i = 0; i < poolAmount; i++)
        {
            if (!poolList[i].activeInHierarchy)
            {
                poolList[i].SetActive(true);
                return poolList[i];
            }
        }
        return null;
    }
}
