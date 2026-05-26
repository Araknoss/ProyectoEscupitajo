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
        List<List<GameObject>> allPools = new()
        {
            poolListA,
            poolListB,
            poolListC,
            poolListD
        };

        List<int> validPools = new();

        // Buscar pools válidas
        for (int i = 0; i < allPools.Count; i++)
        {
            // Evitar repetir la última pool
            if (i == lastPoolIndex)
                continue;

            // Comprobar si tiene algún objeto libre
            bool hasInactiveObject = false;

            foreach (GameObject obj in allPools[i])
            {
                if (!obj.activeInHierarchy)
                {
                    hasInactiveObject = true;
                    break;
                }
            }

            if (hasInactiveObject)
            {
                validPools.Add(i);
            }
        }

        // No hay pools disponibles
        if (validPools.Count == 0)
        {
            Debug.LogWarning("No hay pools con objetos disponibles.");
            return null;
        }

        // Elegir pool aleatoria válida
        int selectedPoolIndex = validPools[Random.Range(0, validPools.Count)];
        List<GameObject> selectedPool = allPools[selectedPoolIndex];

        // Buscar objetos libres
        List<GameObject> availableObjects = new();

        foreach (GameObject obj in selectedPool)
        {
            if (!obj.activeInHierarchy)
            {
                availableObjects.Add(obj);
            }
        }

        // Elegir objeto aleatorio
        GameObject selectedObject =
            availableObjects[Random.Range(0, availableObjects.Count)];

        selectedObject.SetActive(true);

        lastPoolIndex = selectedPoolIndex;

        return selectedObject;
    }
}