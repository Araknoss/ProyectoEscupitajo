using DG.Tweening;
using Rewired;
using UnityEngine;
using UnityEngine.UI;

public class GlyphButtonFeedback : MonoBehaviour
{
    [SerializeField] private RectTransform target;
    [SerializeField] private Image image;

    [SerializeField] private float pressedScale = 0.9f;
    [SerializeField] private float duration = 0.08f;

    private Vector3 originalScale;
    private Color originalColor;

    private void Awake()
    {
        originalScale = target.localScale;

        if (image != null)
            originalColor = image.color;
    }

    private void Start()
    {
        target.DOScale(1.08f, 0.6f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine)
            .SetUpdate(true);
    }

    public void Press()
    {
        target.DOKill();

        Sequence seq = DOTween.Sequence();

        seq.Append(target.DOScale(pressedScale, duration));

        if (image != null)
            seq.Join(image.DOFade(0.7f, duration));

        seq.Append(target.DOScale(originalScale, duration));

        if (image != null)
            seq.Join(image.DOFade(1f, duration));
    }
}