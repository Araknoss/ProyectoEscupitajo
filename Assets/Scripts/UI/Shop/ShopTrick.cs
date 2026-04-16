using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopTrick : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{
    [SerializeField] private Button button;
    [SerializeField] private Color lockedColor;

    [Header("Trick Info")]  
    public Trick shopTrickSO;    
    [SerializeField] private TextMeshProUGUI trickPriceText;
    [SerializeField] private Image trickImage;
    [SerializeField] private Image trickBackgroundImage;

    [Header("Events")]
    public GameEvent onTrickUnlocked;
    public GameEvent onShopTrickSelected;
    
    public void InitializeTrick(Trick trick)
    {
        shopTrickSO = trick;
        if (shopTrickSO == null)
        {
            SetLocked(true);
            return;
        }
        trickPriceText.text = shopTrickSO.cost.ToString() + " G";
        trickImage.sprite = shopTrickSO.sprite;
        if(trickBackgroundImage != null)
        {
            trickBackgroundImage.sprite = shopTrickSO.sprite;
        }            
        CheckIfTrickUnlocked();
    }

    private void CheckIfTrickUnlocked()
    {
        if (UnlockablesManager.Instance.HasUnlockedTrick(shopTrickSO) || shopTrickSO.isUnlockedAtStart)
        {
            SetLocked(false);            
        }
        else
        {
            SetLocked(true);
        }
    }
    private void SetLocked(bool isLocked)
    {
        //button.interactable = !isLocked;
        if (isLocked)
        {
            trickImage.color = lockedColor;
            trickPriceText.gameObject.SetActive(true);
            trickPriceText.text = shopTrickSO.cost.ToString() + " G";
        }
        else
        {
            trickPriceText.gameObject.SetActive(false);
            trickImage.color = Color.white;       
        }

    }

    public void TryToBuy()
    {
        //if (ScoreManager.Instance.gold >= trickSO.cost)
        //{
        //    ScoreManager.Instance.Buy(trickSO.cost);
        //    SetLocked(true);            
        //    trickNameText.text = purchasedText;
        //    trickPriceText.text="";

        //    onTrickUnlocked.Raise(this, trickSO.id);
        //}
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TriggerEvent();
    }

    public void OnSelect(BaseEventData eventData)
    {
        TriggerEvent();
    }

    private void TriggerEvent()
    {        
        Debug.Log("Selected trick: " + shopTrickSO.trickName);
        onShopTrickSelected.Raise(this, shopTrickSO);
    }

}
