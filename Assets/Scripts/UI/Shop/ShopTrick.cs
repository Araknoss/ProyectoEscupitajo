using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopTrick : MonoBehaviour
{
    [SerializeField] private Trick trickSO;
    [SerializeField] private TextMeshProUGUI trickNameText;
    [SerializeField] private TextMeshProUGUI trickPriceText;
    private Button button;

    [SerializeField] private string purchasedText;


    [Header("Internal Variables")]    
    private bool isPurchased = false;
    public bool isLocked = false;
    private void Start()
    {
        button = gameObject.GetComponent<Button>();
        SetLocked(isLocked);

        if (trickSO == null)
        {
            SetLocked(true);
            return;
        }

        trickNameText.text = trickSO.trickName;    
        trickPriceText.text = trickSO.cost.ToString() + " G";
    }

    private void SetLocked(bool locked)
    {
        isLocked = locked;
        button.interactable = !isLocked;
    }

    public void TryToBuy()
    {
        if (ScoreManager.Instance.gold >= trickSO.cost)
        {
            ScoreManager.Instance.Buy(trickSO.cost);
            SetLocked(true);
            isPurchased = true;
            //trickSO.isPurchased = true; No funciona correctament aqui
            trickNameText.text = purchasedText;
            trickPriceText.text="";
        }
    }
}
