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
    [SerializeField] private GameEvent onCantBuyTrick;
    [SerializeField] private Trick initialTrick;

    [Header("Variables")]
    [SerializeField] private Color lockedColor;
    [SerializeField] private Color unlockedColor = Color.white;

    [Header("UnknownVariables")]
    [SerializeField] private string unknownName = "???";
    [SerializeField] private Sprite unknownSprite;


    private void OnEnable()
    {
        AssignValues(this, initialTrick);
    }
    public void AssignValues(Component sender, object data)
    {
        if (data is Trick)
        {
            actualTrick = (Trick)data;
            if (actualTrick == null) return;

            // Seguridad: comprobar cada componente antes de asignar
            if (trickNameText != null)
                trickNameText.text = actualTrick.trickName ?? string.Empty;

            if (trickPriceText != null)
                trickPriceText.text = actualTrick.cost.ToString() + " G";

            if (trickSprite != null)
                trickSprite.sprite = actualTrick.sprite;

            if (trickBaseScoreText != null)
                trickBaseScoreText.text = actualTrick.baseScore.ToString();

            if (trickHardnessText != null)
                trickHardnessText.text = actualTrick.hardness.ToString();

            bool isUnlocked = false;
            if (UnlockablesManager.Instance != null)
            {
                isUnlocked = UnlockablesManager.Instance.HasUnlockedTrick(actualTrick);
            }

            if (isUnlocked || actualTrick.isUnlockedAtStart)
            {
                if(trickSprite != null)
                    trickSprite.color = unlockedColor;
                SetBuyButtonActive(false);
            }
            else
            {
                if (trickSprite != null)
                    trickSprite.color = lockedColor;
                SetBuyButtonActive(true);
            }
        }
        else
        {
            actualTrick = null;

            if (trickNameText != null)
                trickNameText.text = unknownName;

            if (trickPriceText != null)
                trickPriceText.text = string.Empty;

            if (trickSprite != null)
                trickSprite.sprite = unknownSprite;

            if (trickBaseScoreText != null)
                trickBaseScoreText.text = string.Empty;

            if (trickHardnessText != null)
                trickHardnessText.text = string.Empty;

            SetBuyButtonActive(false);
        }
    }

    public void SetBuyButtonActive(bool isActive)
    {
        if (buyButton != null)
            buyButton.gameObject.SetActive(isActive);

        if (trickPriceText != null)
            trickPriceText.gameObject.SetActive(isActive);
    }

    public void TryBuyActualTrick()
    {
        if (actualTrick == null) return;

        if (GoldManager.Instance != null && GoldManager.Instance.gold >= actualTrick.cost)
        {
            Debug.Log("Attempting to buy trick: " + actualTrick.trickName);
            if (UnlockablesManager.Instance != null && !UnlockablesManager.Instance.HasUnlockedTrick(actualTrick) && !actualTrick.isUnlockedAtStart)
            {
                Debug.Log("Buying trick: " + actualTrick.trickName);
                GoldManager.Instance.Buy(actualTrick.cost);
                UnlockablesManager.Instance.UnlockTrick(this, actualTrick.id);
                if (onTrickUnlocked != null)
                    onTrickUnlocked.Raise(this, actualTrick);
                SetBuyButtonActive(false);
            }
        }
        else
        {                      
            if (onCantBuyTrick != null)
                onCantBuyTrick.Raise(this, actualTrick);
        }
    }
}
