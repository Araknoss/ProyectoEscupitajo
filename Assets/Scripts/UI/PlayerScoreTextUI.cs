using DG.Tweening;
using MoreMountains.Feedbacks;
using TMPro;
using UnityEngine;

public class PlayerScoreTextUI : MonoBehaviour
{
    [Header("Animation")]    
    [SerializeField] private float stayDuration = 1f;
    [SerializeField] private float hideDuration = 0.2f;
    [SerializeField] private float returnAnimationSpeed = 5f;
    private float stayTimer=0;
    [SerializeField] private float scaleUpAmount = 1.2f;

    [Header("References")]    
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private CanvasGroup canvasGroup;
    private Tween currentTween;

    [Header("Color")]
    [SerializeField] private Color okColor = Color.green;
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color perfectColor = Color.red;

    [Header("Feedback")]
    [SerializeField] private MMF_Player comboEndFeedback;
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

    public void HandleComboEnd(Component sender, object data)
    {
        stayTimer = stayDuration;            
    }    

    public void HandleOnTrickFailed(Component sender, object data)
    {
        //stayTimer = stayDuration;
        if (comboEndFeedback != null)
        {
            comboEndFeedback.PlayFeedbacks();
        }
    }

    private void EndAnimation()
    {        
        text.alpha = 0f;               
        //text.gameObject.SetActive(false);
    }

    private void AddTemporaryScore(int score) 
    {
        text.transform.localScale = Vector3.one * scaleUpAmount;
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
