using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
                buyButton.gameObject.SetActive(false);
                trickPriceText.gameObject.SetActive(false);
            }
            else
            {
                buyButton.gameObject.SetActive(true);
                trickPriceText.gameObject.SetActive(true);
            }
        }       
    }

    public void TryBuyActualTrick()
    {
        if (GoldManager.Instance.gold >= actualTrick.cost)
        {
            if (UnlockablesManager.Instance.HasUnlockedTrick(actualTrick) || actualTrick.isUnlockedAtStart)
            {
                GoldManager.Instance.Buy(actualTrick.cost);                
                UnlockablesManager.Instance.UnlockTrick(this, actualTrick.id);
            }            
        }

    }
}
