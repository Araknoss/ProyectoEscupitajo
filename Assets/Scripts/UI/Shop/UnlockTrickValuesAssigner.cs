using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class UnlockTrickValuesAssigner : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI trickNameText;
    [SerializeField] private Image trickImage;
    public void AssignValues(Component sender, object data)
    {
        if (data is Trick)
        {
            Trick trick = (Trick)data;
            trickNameText.text = trick.trickName;
            trickImage.sprite = trick.sprite;
        }
    }   
}
