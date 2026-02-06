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

    [Header("Events")]
    public GameEvent onTrickUnlocked;   
    private void Start()
    {
        button = gameObject.GetComponent<Button>();        
        
        if (trickSO == null)
        {
            SetLocked(true);
            return;
        }

        CheckIfTrickUnlocked();       
    }

    private void CheckIfTrickUnlocked()
    {
        if (UnlockablesManager.Instance.HasUnlockedTrick(trickSO))
        {
            SetLocked(true);        
        }
        else
        {
            SetLocked(false);
            trickNameText.text = trickSO.trickName;
            trickPriceText.text = trickSO.cost.ToString() + " G";
        }
    }
    private void SetLocked(bool isLocked)
    {     
        button.interactable = !isLocked;
    }

    public void TryToBuy()
    {
        if (ScoreManager.Instance.gold >= trickSO.cost)
        {
            ScoreManager.Instance.Buy(trickSO.cost);
            SetLocked(true);            
            trickNameText.text = purchasedText;
            trickPriceText.text="";

            onTrickUnlocked.Raise(this, trickSO.id);
        }
    }
}
