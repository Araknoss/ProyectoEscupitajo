using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShopTreeTricksAssigner : MonoBehaviour
{
    private List<ShopTrick> childrenShopTrick = new List<ShopTrick>();
    [SerializeField] private ShopTreeDataSO shopTreeData;

    private void Awake()
    {
        GetShopTricksInChildren();
    }
    private void GetShopTricksInChildren()
    {        
        for (int i = 0; i < transform.childCount; i++)
        {
            ShopTrick st = transform.GetChild(i).GetComponent<ShopTrick>();
            if (st != null)
            {
                childrenShopTrick.Add(st);
            }
        }

        AssignTricksToChildren(shopTreeData.tricksInTree);
    }

    public void AssignTricksToChildren(List<Trick> newTricks)
    {
        // 1. Ordenar por ID
        //newTricks.Sort((a, b) => a.id.CompareTo(b.id));

        for (int i = 0; i < childrenShopTrick.Count; i++)
        {
            if (childrenShopTrick != null && i < newTricks.Count)
            {
                childrenShopTrick[i].InitializeTrick(newTricks[i]);
            }
            else if (childrenShopTrick != null)
            {
                childrenShopTrick[i].InitializeTrick(null); // Si no hay truco, inicializar con null para bloquearlo
            }
        }      
    }
}
