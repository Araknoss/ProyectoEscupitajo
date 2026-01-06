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

    private float temporaryScore = 0f;

    [Header("References")]    
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private CanvasGroup canvasGroup;
    private Tween currentTween;

    private void Update()
    {
        text.transform.localScale = Vector3.Lerp(text.transform.localScale, Vector3.one, Time.deltaTime * returnAnimationSpeed);
    }
    public void HandleTrickPerformed(Component sender, object data)
    {
        if (data is Trick)
        {
            Trick trick = (Trick)data;
            AddTemporaryScore(trick.baseScore);

            currentTween?.Kill();
            PlayAnimation();
        }
    }
    private void PlayAnimation()
    {       
        //Reset state
        currentTween?.Kill();

        text.gameObject.SetActive(true);        
        canvasGroup.alpha = 1f;
        text.transform.localScale = Vector3.one * 1.3f;        

        Sequence seq = DOTween.Sequence();

        seq.AppendInterval(stayDuration)
           .Append(canvasGroup.DOFade(0f, hideDuration))
           .OnComplete(() => EndAnimation());

        currentTween = seq;
    }

    private void EndAnimation()
    {
        text.gameObject.SetActive(false);
        temporaryScore = 0f;
        currentTween?.Kill();
    }

    private void AddTemporaryScore(int score)
    {
        temporaryScore += score;
        text.text = "+" + temporaryScore.ToString();
    }
}
