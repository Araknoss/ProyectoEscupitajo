using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopTrick : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{
    [SerializeField] private Button button;
    [SerializeField] private Color lockedColor;
    [SerializeField] private Animator animator;
    [SerializeField] private AnimatorController unlockedAnimator;
    [SerializeField] private AnimatorController lockedAnimator;
    public bool isLocked = true;

    [Header("Trick Info")]  
    public Trick shopTrickSO;    
    [SerializeField] private TextMeshProUGUI trickPriceText;
    [SerializeField] private Image trickImage;
    [SerializeField] private Image trickBackgroundImage;

    [Header("Events")]    
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
    private void SetLocked(bool trickLocked)
    {
        //button.interactable = !isLocked;
        isLocked=trickLocked;
        if (trickLocked)
        {            
            trickImage.color = lockedColor;
            trickPriceText.gameObject.SetActive(true);
            trickPriceText.text = shopTrickSO.cost.ToString() + " G";
            animator.runtimeAnimatorController = lockedAnimator;

        }
        else
        {
            trickPriceText.gameObject.SetActive(false);
            trickImage.color = Color.white;       
            animator.runtimeAnimatorController = unlockedAnimator;
        }

    }

    public void TryToBuy()
    {
        //En shoptrickvaluesassigner
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

    public void HandleOnTrickUnlocked(Component sender, object data)
    {
        if(data is Trick unlockedTrick)
        {
            if(unlockedTrick == shopTrickSO)
            {
                SetLocked(false);
            }
        }
    }
}
