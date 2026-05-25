using System.Collections.Generic;
using UnityEngine;

public class MainBuildingPooler : MonoBehaviour
{
    [Header("Pools")]
    [SerializeField] private List<GameObject> poolListA = new List<GameObject>();
    [SerializeField] private List<GameObject> poolListB = new List<GameObject>();
    [SerializeField] private List<GameObject> poolListC = new List<GameObject>();
    [SerializeField] private List<GameObject> poolListD = new List<GameObject>();

    private int lastPoolIndex = -1;

    private void Awake()
    {
        InitializePool(poolListA);
        InitializePool(poolListB);
        InitializePool(poolListC);
        InitializePool(poolListD);
    }

    private void InitializePool(List<GameObject> pool)
    {
        foreach (GameObject obj in pool)
        {
            obj.SetActive(false);
        }
    }

    public GameObject GetChunk()
    {
        List<List<GameObject>> allPools = new List<List<GameObject>>
        {
            poolListA,
            poolListB,
            poolListC,
            poolListD
        };

        // Crear lista de pools válidas (que no sean la última usada)
        List<int> validPools = new List<int>();

        for (int i = 0; i < allPools.Count; i++)
        {
            if (i != lastPoolIndex && allPools[i].Count > 0)
            {
                validPools.Add(i);
            }
        }

        // Elegir una pool aleatoria válida
        int selectedPoolIndex = validPools[Random.Range(0, validPools.Count)];
        List<GameObject> selectedPool = allPools[selectedPoolIndex];

        // Elegir objeto aleatorio dentro de la pool
        for(int i = 0; i < selectedPool.Count; i++)
        {
            int randomIndex = Random.Range(0, selectedPool.Count);
            if (!selectedPool[randomIndex].activeInHierarchy)
            {
                selectedPool[randomIndex].SetActive(true);
                lastPoolIndex = selectedPoolIndex;
                return selectedPool[randomIndex];
            }
        }
        //GameObject selectedObject = selectedPool[Random.Range(0, selectedPool.Count)];

        //lastPoolIndex = selectedPoolIndex;

        //selectedObject.SetActive(true);
        return null;
    }
}