using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBuildingPooler : MonoBehaviour
{
    [Header("Pool Lists")]
    [SerializeField] private List<GameObject> poolList1 = new List<GameObject>();
    [SerializeField] private List<GameObject> poolList2 = new List<GameObject>();
    [SerializeField] private List<GameObject> poolList3 = new List<GameObject>();
    [SerializeField] private List<GameObject> poolList4 = new List<GameObject>();

    [SerializeField] private int poolSize = 5;

    public GameObject lastChunk;

    private int lastUsedList = -1;

    //void Awake()
    //{
    //    CreatePool();
    //}

    //private void CreatePool()
    //{
    //    for (int i = 0; i < transform.childCount; i++)
    //    {
    //        GameObject obj = transform.GetChild(i).gameObject;

    //        obj.SetActive(false);

    //        int listIndex = i % 4;

    //        switch (listIndex)
    //        {
    //            case 0:
    //                poolList1.Add(obj);
    //                break;

    //            case 1:
    //                poolList2.Add(obj);
    //                break;

    //            case 2:
    //                poolList3.Add(obj);
    //                break;

    //            case 3:
    //                poolList4.Add(obj);
    //                break;
    //        }
    //    }
    //}

    public GameObject GetRandomPooledObject()
    {
        List<List<GameObject>> availableLists = new List<List<GameObject>>();
        List<int> availableIndexes = new List<int>();

        // Añade todas las listas excepto la última usada
        if (lastUsedList != 0)
        {
            availableLists.Add(poolList1);
            availableIndexes.Add(0);
        }

        if (lastUsedList != 1)
        {
            availableLists.Add(poolList2);
            availableIndexes.Add(1);
        }

        if (lastUsedList != 2)
        {
            availableLists.Add(poolList3);
            availableIndexes.Add(2);
        }

        if (lastUsedList != 3)
        {
            availableLists.Add(poolList4);
            availableIndexes.Add(3);
        }

        // Elegimos una lista aleatoria válida
        int selectedListIndex = Random.Range(0, availableLists.Count);

        List<GameObject> selectedList = availableLists[selectedListIndex];

        // Buscamos un objeto inactivo dentro de esa lista
        for (int i = 0; i < selectedList.Count; i++)
        {
            int randomObjectIndex = Random.Range(0, selectedList.Count);

            if (!selectedList[randomObjectIndex].activeInHierarchy)
            {
                selectedList[randomObjectIndex].SetActive(true);

                // Guardamos qué lista se ha usado
                lastUsedList = availableIndexes[selectedListIndex];

                return selectedList[randomObjectIndex];
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
