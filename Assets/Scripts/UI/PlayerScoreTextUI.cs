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
    private float temporaryScore = 0f;

    [Header("References")]    
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private CanvasGroup canvasGroup;
    private Tween currentTween;

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
            Trick trick = (Trick)data;
            AddTemporaryScore(trick.baseScore);

            //currentTween?.Kill();
            //PlayAnimation();
        }
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
        temporaryScore = 0f;        
        text.gameObject.SetActive(false);
    }

    private void AddTemporaryScore(int score)
    {
        text.transform.localScale = Vector3.one * 1.3f;
        stayTimer = 0f;
        text.gameObject.SetActive(true);
        text.alpha = 1f;
        temporaryScore += score;
        text.text = "+" + temporaryScore.ToString();
    }
}
