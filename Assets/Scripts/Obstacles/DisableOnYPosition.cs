using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnYPosition : MonoBehaviour
{  
    [SerializeField] private float disableYValue;
    private void Update()
    {
        float yPosition = gameObject.transform.position.y;
        if(yPosition>= disableYValue)
        {
            gameObject.SetActive(false);
        }
    }
}
