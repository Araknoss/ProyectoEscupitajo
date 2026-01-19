using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopTrick : MonoBehaviour
{
    [SerializeField] private Trick trickSO;
    private TextMeshProUGUI trickText;
    private Button button;


    [Header("Internal Variables")]    
    private bool isPurchased = false;
    public bool isLocked = false;
    private void Start()
    {
        trickText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        button = gameObject.GetComponent<Button>();
        SetLocked(isLocked);

        if (trickSO == null)
        {
            SetLocked(true);
            return;
        }

        trickText.text = trickSO.trickName;

        
    }

    private void SetLocked(bool locked)
    {
        isLocked = locked;
        button.interactable = !isLocked;
    }
}
