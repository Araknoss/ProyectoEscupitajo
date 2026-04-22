using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopTrickValuesAssigner : MonoBehaviour
{
    [Header("Trick Info")]
    [SerializeField] private Trick actualTrick;

    [SerializeField] private TextMeshProUGUI trickNameText;
    [SerializeField] private TextMeshProUGUI trickPriceText;
    [SerializeField] private Image trickSprite;
    [SerializeField] private TextMeshProUGUI trickBaseScoreText;
    [SerializeField] private TextMeshProUGUI trickHardnessText;

    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private Button buyButton;
    [SerializeField] private GameEvent onTrickUnlocked;
    public void AssignValues(Component sender, object data)
    {
        if(data is Trick)
        {
            actualTrick = (Trick)data;
            trickNameText.text = actualTrick.trickName;
            trickPriceText.text = actualTrick.cost.ToString() + " G";
            trickSprite.sprite = actualTrick.sprite;
            trickBaseScoreText.text = actualTrick.baseScore.ToString();
            trickHardnessText.text = actualTrick.hardness.ToString();

            if (UnlockablesManager.Instance.HasUnlockedTrick(actualTrick) || actualTrick.isUnlockedAtStart)
            {
                SetBuyButtonActive(false);
            }
            else
            {
                SetBuyButtonActive(true);
            }
        }       
    }

    public void SetBuyButtonActive(bool isActive)
    {
        buyButton.gameObject.SetActive(isActive);
        trickPriceText.gameObject.SetActive(isActive);
    }

    public void TryBuyActualTrick()
    {
        if (GoldManager.Instance.gold >= actualTrick.cost)
        {
            Debug.Log("Attempting to buy trick: " + actualTrick.trickName);
            if (!UnlockablesManager.Instance.HasUnlockedTrick(actualTrick) && !actualTrick.isUnlockedAtStart)
            {
                Debug.Log("Buying trick: " + actualTrick.trickName);
                GoldManager.Instance.Buy(actualTrick.cost);                
                UnlockablesManager.Instance.UnlockTrick(this, actualTrick.id);
                onTrickUnlocked.Raise(this, actualTrick);
                SetBuyButtonActive(false);
            }            
        }

    }
}
