using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScoreTextUI : MonoBehaviour
{
    [Header("Settings")]
    public float showDuration = 0.05f;
    public float stayDuration = 1f;
    public float hideDuration = 0.2f;
    public float returnAnimationSpeed = 5f;

    [Header("References")]    
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private CanvasGroup canvasGroup;
    private Tween currentTween;

    private void Update()
    {
        text.transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime * returnAnimationSpeed);
    }
    public void HandleTrickPerformed(Component sender, object data)
    {
        if (data is Trick)
        {
            Trick trick = (Trick)data;
            text.text = "+ " + trick.baseScore.ToString();

            text.gameObject.SetActive(true);

            currentTween?.Kill();

            PlayAnimation();
        }
    }
    private void PlayAnimation()
    {
        // Reset state
        canvasGroup.alpha = 1f;
        text.transform.localScale = Vector3.one * 1.3f;

        // Sequence
        Sequence seq = DOTween.Sequence();

        seq.AppendInterval(stayDuration)
           .Append(canvasGroup.DOFade(0f, hideDuration))           
           .OnComplete(() => text.gameObject.SetActive(false));

        currentTween = seq;
    }
}
