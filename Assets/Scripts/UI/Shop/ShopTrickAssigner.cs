using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrickAssigner : MonoBehaviour
{
    [SerializeField] private List<Trick> tricksToAssign; //0
    [SerializeField] private List<Trick> tricksToAssign2; //1
    [SerializeField] private List<Trick> tricksToAssign3; //2
    [SerializeField] private int currentListIndex = 0;

    private List<ShopTrick> childrenShopTrick = new List<ShopTrick>();

    [SerializeField] private DynamicButtonNavigation _gridNavigation;
    private void Start()
    {
        AssignChildren();
        AssignTricks(GetCurrentTrickList());        
    }

    public void AddIndex()
    {
        currentListIndex = (currentListIndex + 1) % 3; // Asumiendo que solo hay 3 listas
        AssignTricks(GetCurrentTrickList());
    }

    public void SubtractIndex()
    {
        currentListIndex = (currentListIndex - 1 + 3) % 3; // Para evitar índices negativos
        AssignTricks(GetCurrentTrickList());
    }
    private List<Trick> GetCurrentTrickList()
    {
        switch (currentListIndex)
        {
            case 0:
                return tricksToAssign;
            case 1:
                return tricksToAssign2;
            case 2:
                return tricksToAssign3;
            default:
                return new List<Trick>();
        }
    }
    private void AssignChildren()
    {
        childrenShopTrick.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            ShopTrick st = transform.GetChild(i).GetComponent<ShopTrick>();
            if (st != null)
            {
                childrenShopTrick.Add(st);
            }
        }
    }
    public void AssignTricks(List<Trick> newTricks)
    {
        // 1. Ordenar por ID
        newTricks.Sort((a, b) => a.id.CompareTo(b.id));

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

        _gridNavigation.RefreshNavigation();
    }
}
