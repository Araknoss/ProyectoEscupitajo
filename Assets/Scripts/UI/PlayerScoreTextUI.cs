using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class PlayerScoreTextUI : MonoBehaviour
{
    [Header("Animation")]    
    [SerializeField] private float stayDuration = 1f;
    [SerializeField] private float hideDuration = 0.2f;
    [SerializeField] private float returnAnimationSpeed = 5f;
    private float stayTimer=0;    

    [Header("References")]    
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private CanvasGroup canvasGroup;
    private Tween currentTween;

    [Header("Color")]
    [SerializeField] private Color okColor = Color.green;
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color perfectColor = Color.red;
    private void Update()
    {
        text.transform.localScale = Vector3.Lerp(text.transform.localScale, Vector3.one, Time.deltaTime * returnAnimationSpeed);

        stayTimer+= Time.deltaTime;

        if (stayTimer > stayDuration)
        {
            text.alpha = Mathf.Lerp(text.alpha, 0f, Time.deltaTime * 20);
        }
        if(stayTimer>=stayDuration+hideDuration)
        {
            EndAnimation();
        }
    }
    public void HandleTrickPerformed(Component sender, object data)
    {
        if (data is Trick)
        {
            //Trick trick = (Trick)data;
            //stayDuration = trick.listenInputTime - hideDuration;
            //AddTemporaryScore(trick.baseScore, trick); 
        }
    }

    public void HandleComboEnd(Component sender, object data)
    {
        stayTimer = stayDuration;              
    }
    //private void PlayAnimation()
    //{       
    //    //Reset state
    //    currentTween?.Kill();

    //    text.gameObject.SetActive(true);        
    //    canvasGroup.alpha = 1f;
    //    text.transform.localScale = Vector3.one * 1.3f;        

    //    Sequence seq = DOTween.Sequence();

    //    seq.AppendInterval(stayDuration)
    //       .Append(canvasGroup.DOFade(0f, hideDuration))
    //       .OnComplete(() => EndAnimation());

    //    currentTween = seq;
    //}

    private void EndAnimation()
    {        
        text.alpha = 0f;               
        text.gameObject.SetActive(false);
    }

    private void AddTemporaryScore(int score) 
    {
        text.transform.localScale = Vector3.one * 1.3f;
        stayTimer = 0f;
        text.gameObject.SetActive(true);
        text.alpha = 1f;           
        text.text = "+" + score.ToString();
    }

    public void HandleAddTemporaryScore(Component sender, object data)
    {
        if (data is int)
        {
            int score = (int)data;
            AddTemporaryScore(score);
        }
    }
}
