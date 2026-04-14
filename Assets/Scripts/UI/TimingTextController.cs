using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimingTextController : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private float stayDuration = 1f;
    [SerializeField] private float hideDuration = 0.2f;
    [SerializeField] private float returnAnimationSpeed = 5f;
    private float stayTimer = 0;
    [SerializeField] private float scaleUpAmount = 1.2f;

    [Header("References")]
    [SerializeField] private TextMeshProUGUI timingText;    

    [Header("Color")]
    [SerializeField] private Color greatColor = Color.green;
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color perfectColor = Color.red;

    [Header("Messages")]
    [SerializeField] private string greatTimingMessage = "Great!";
    [SerializeField] private string perfectTimingMessage = "Perfect!";
    private void Update()
    {
        timingText.transform.localScale = Vector3.Lerp(timingText.transform.localScale, Vector3.one, Time.deltaTime * returnAnimationSpeed);

        stayTimer += Time.deltaTime;

        if (stayTimer > stayDuration)
        {
            timingText.alpha = Mathf.Lerp(timingText.alpha, 0f, Time.deltaTime * 20);
        }
        if (stayTimer >= stayDuration + hideDuration)
        {
            EndAnimation();
        }
    }    

    public void HandleComboEnd(Component sender, object data)
    {
        stayTimer = stayDuration;
    }    

    private void EndAnimation()
    {
        timingText.alpha = 0f;
        //timingText.gameObject.SetActive(false);
    }

    private void PlayTextAnimation(string newTimingText, Color color)
    {
        timingText.transform.localScale = Vector3.one * scaleUpAmount;
        stayTimer = 0f;
        timingText.text = newTimingText;
        timingText.gameObject.SetActive(true);
        timingText.color = color;           
    }    
    public void OnTrickPerformedOnGreatTiming(Component sender, object data)
    {        
        if(data is bool)   
        {
            PlayTextAnimation(greatTimingMessage, greatColor);
            Debug.Log("Great Timing TEXT!");
        }       
        
    }

    public void OnTrickPerformedOnPerfectTiming(Component sender, object data)
    {
        if(data is bool)
        {                      
            PlayTextAnimation(perfectTimingMessage, perfectColor);
        }         
    }
}
