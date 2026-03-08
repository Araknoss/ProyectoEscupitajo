using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimingTextController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timingText;
    [SerializeField] private float fadeOutSpeed = 1f;
    private float fadeOutTimer =1f;

    [SerializeField] private string perfectTimingMessage = "Perfect!";
    [SerializeField] private string okTimingMessage = "OK!";
    private void Update()
    {
        fadeOutTimer = Mathf.Clamp01( fadeOutTimer - Time.deltaTime);
        if(fadeOutTimer <= 0)
        {
            timingText.alpha = 0;
        }
    }
    public void OnTrickPerformed(Component sender, object data)
    {        
        if(data is bool)   
        {
            timingText.alpha = 1;
            fadeOutTimer = 1f;

            bool isPerfectTiming = (bool)data;
            if(isPerfectTiming)
            {
                timingText.text = perfectTimingMessage;
            }
            else 
            { 
                timingText.text = okTimingMessage;
            }
        }
        

        //Play animation
    }
}
