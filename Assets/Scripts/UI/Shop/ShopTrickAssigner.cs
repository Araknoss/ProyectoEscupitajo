using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrickAssigner : MonoBehaviour
{
    [SerializeField] private List<Trick> tricksToAssign;

    private void Start()
    {
        // 1. Ordenar por ID
        tricksToAssign.Sort((a, b) => a.id.CompareTo(b.id));        
       
        for(int i=0; i<tricksToAssign.Count; i++)
        {
            ShopTrick st = transform.GetChild(i).GetComponent<ShopTrick>();
            if (st != null)
            {
                st.InitializeTrick(tricksToAssign[i]);
            }
        }              
    }
}
