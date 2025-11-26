using UnityEngine;
using TMPro;
using DG.Tweening;
using JetBrains.Annotations;

public class TrickScoreTextUI : MonoBehaviour
{
    [Header("Settings")]
    public float showDuration = 0.2f;
    public float stayDuration = 1f;
    public float hideDuration = 0.2f;

    [Header("References")]
    [SerializeField] private GameObject parent;
    public TextMeshProUGUI text;
    private CanvasGroup canvasGroup;
    private Tween currentTween;

    private void Awake()
    {
        //// Add CanvasGroup if missing
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();

    }

    private void OnEnable()
    {      
        
        PlayAnimation();
    }

    private void OnDisable()
    {
        // Safety: kill tween if object is disabled early
        currentTween?.Kill();
    }
    public void SetTrickScoreText(object data)
    {
        if (data is Trick)
        {
            Trick trick = (Trick)data;
            text.text = "+ "+trick.baseScore.ToString();
        }
    }
    private void PlayAnimation()
    {
        // Reset state
        canvasGroup.alpha = 0f;        
        transform.localScale = Vector3.zero;

        // Sequence
        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOScale(1f, showDuration).SetEase(Ease.OutBack))
           .Join(canvasGroup.DOFade(1f, showDuration))
           .AppendInterval(stayDuration)
           .Append(canvasGroup.DOFade(0f, hideDuration))
           .Join(transform.DOScale(0.8f, hideDuration))
           .OnComplete(() => parent.SetActive(false));

        currentTween = seq;
    }
}

