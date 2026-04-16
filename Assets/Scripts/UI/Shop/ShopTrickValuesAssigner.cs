using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopTrickValuesAssigner : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI trickNameText;
    [SerializeField] private TextMeshProUGUI trickPriceText;
    [SerializeField] private Image trickSprite;
    [SerializeField] private TextMeshProUGUI trickBaseScoreText;
    [SerializeField] private TextMeshProUGUI trickHardnessText;

    [SerializeField] private Animator animator;
    public void AssignValues(Component sender, object data)
    {
        if(data is Trick)
        {
            Trick trick = (Trick)data;
            trickNameText.text = trick.trickName;
            trickPriceText.text = trick.cost.ToString() + " G";
            trickSprite.sprite = trick.sprite;
            trickBaseScoreText.text = trick.baseScore.ToString();
            trickHardnessText.text = trick.hardness.ToString();

            animator.Rebind();
            if (trick.uiClip != null)
                animator.Play(trick.uiClip.name);
        }       
    }
}
