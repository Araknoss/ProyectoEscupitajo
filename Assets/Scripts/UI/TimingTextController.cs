using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimingTextController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timingText;
    [SerializeField] private float fadeOutSpeed = 1f;
    [SerializeField] private float fadeOutTime =0.3f;

    [SerializeField] private string perfectTimingMessage = "Perfect!";
    [SerializeField] private string greatTimingMessage = "Great";
    private void Update()
    {
        fadeOutTime = Mathf.Clamp01( fadeOutTime - Time.deltaTime);
        if(fadeOutTime <= 0)
        {
            timingText.alpha = 0;
        }
    }
    public void OnTrickPerformedOnGreatTiming(Component sender, object data)
    {        
        if(data is bool)   
        {
            timingText.alpha = 1;
            fadeOutTime = 1f;
            timingText.text = greatTimingMessage;
        }       

        //Play animation
    }

    public void OnTrickPerformedOnPerfectTiming(Component sender, object data)
    {
        if(data is bool)
        {
            timingText.alpha = 1;
            fadeOutTime = 1f;
            timingText.text = perfectTimingMessage;
        }           

        //Play animation
    }
}
