using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Animations;

public class ShopTrick : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{
    [SerializeField] private Button button;
    [SerializeField] private Color lockedColor;
    [SerializeField] private Animator animator;
    [SerializeField] private RuntimeAnimatorController unlockedAnimator;
    [SerializeField] private RuntimeAnimatorController lockedAnimator;
    [SerializeField] private Image unknownImage;
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
        if (shopTrickSO == null) //Para cuando no haya truco asignado, como en los espacios vacíos de la tienda
        {
            SetNull();
            button.interactable = false;
            return;
        }
        else
        {
            button.interactable = true;
        }

            trickPriceText.text = shopTrickSO.cost.ToString() + " G";
        trickImage.sprite = shopTrickSO.sprite;
        if(trickBackgroundImage != null)
        {
            trickBackgroundImage.sprite = shopTrickSO.sprite;
        }            
        CheckIfTrickUnlocked();
    }

    //private void OnEnable()
    //{
    //    CheckIfTrickUnlocked();
    //}

    public void CheckIfTrickUnlocked()
    {
        if (UnlockablesManager.Instance.HasUnlockedTrick(shopTrickSO) || shopTrickSO.isUnlockedAtStart)
        {
            SetLocked(false);            
        }
        else
        {
            Trick previousTrick = button.navigation.selectOnUp.GetComponent<ShopTrick>().shopTrickSO;
            if (previousTrick != null && !UnlockablesManager.Instance.HasUnlockedTrick(previousTrick) && !previousTrick.isUnlockedAtStart)
            {
                
                    SetUnknown();
                    return;
                                
            }
            else
            {
                SetLocked(true);
            }              
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
            unknownImage.gameObject.SetActive(false);
            button.interactable = true;

        }
        else
        {
            trickPriceText.gameObject.SetActive(false);
            trickImage.color = Color.white;       
            animator.runtimeAnimatorController = unlockedAnimator;
            unknownImage.gameObject.SetActive(false);
            button.interactable = true;
        }


    }

    private void SetUnknown()
    {
        isLocked = true;
        trickImage.color = Color.clear;
        trickPriceText.gameObject.SetActive(false);
        animator.runtimeAnimatorController = lockedAnimator;
        unknownImage.gameObject.SetActive(true);       
        button.interactable = false;
    }

    private void SetNull()
    {
        isLocked = true;
        trickImage.color = Color.clear;
        trickPriceText.gameObject.SetActive(false);
        animator.runtimeAnimatorController = lockedAnimator;        
    }

    public void TryToBuy()
    {
        //En shoptrickvaluesassigner
    }  

    public void OnPointerEnter(PointerEventData eventData)
    {   
        if (EventSystem.current.currentSelectedGameObject == gameObject) return;
        if (!button.interactable) return;
        TriggerEvent();
        EventSystem.current.SetSelectedGameObject(gameObject);
    }
    public void OnSelect(BaseEventData eventData)
    {
        if(!button.interactable) return;
        TriggerEvent();
    }  
    private void TriggerEvent()
    {        
        //Debug.Log("Selected trick: " + shopTrickSO.trickName);
        onShopTrickSelected.Raise(this, shopTrickSO);
      
    }

    public void HandleOnTrickUnlocked(Component sender, object data)
    {
        if (data is Trick unlockedTrick)
        {
            if (unlockedTrick == shopTrickSO)
            {
                SetLocked(false);                
            }
        }
    }
}
