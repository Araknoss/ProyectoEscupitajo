using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateGoldOnEnable : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI targetText;
    [SerializeField] private bool showGoldFromScore; //Si es true, muestra el oro ganado en la partida, si es false, muestra el oro total
    private void OnEnable()
    {
        if(GoldManager.Instance == null || targetText == null) return;
        if(showGoldFromScore)
        {
            targetText.text = GoldManager.Instance.goldFromScore.ToString() + " G";
            return;
        }
        targetText.text = GoldManager.Instance.gold.ToString() +" G";

    }
}
