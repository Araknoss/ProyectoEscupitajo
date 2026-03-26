using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBarController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TrickManager trickManager;
    [SerializeField] private Image barImage;
    [SerializeField] private RectTransform movingIndicator;
    [SerializeField] private RectTransform indicatorArea;

    [Header("Bar Timing")]
    private float timer;
    private bool isRunning;
    private float duration;

    private bool isKeepTrickActive;
    private float indicatorNormalizedPosition; // 0 -> left, 1 -> right    

    [SerializeField] private float barImageWidthPercentageOnCharge=0.7f;

    private float barImageOriginalWidth;

    [Header("Color")]
    [SerializeField] private Color greatColor = Color.green;
    [SerializeField] private Color perfectColor = Color.red;
    [SerializeField] private Color nullColor = Color.gray;
    [SerializeField] private Color noColor = Color.clear;
    [SerializeField] private Color keepColor;

    private void Start()
    {
        barImageOriginalWidth = barImage.rectTransform.rect.width;
        indicatorArea=barImage.rectTransform;
    }
    private void Update()
    {
        if (isRunning)
        {
            UpdateBarFill();
        }

        if (isKeepTrickActive)
        {
            UpdateMovingIndicator();
        }
    }

    private void UpdateBarFill()
    {
        if (barImage == null) return;

        timer += Time.deltaTime;

        float normalized = 1f - (timer / duration);
        normalized = Mathf.Clamp01(normalized);

        barImage.fillAmount = normalized;

        if (timer >= duration)
        {
            isRunning = false;
            barImage.fillAmount = 0f;           
        }
    }

    public void StartBar(float timeToEmpty, Color startColor)
    {
        //Debug.Log("StartBar");
        duration = timeToEmpty;
        timer = 0f;
        isRunning = true;
        barImage.fillAmount = 1f;
        SetBarColor(startColor);
    }

    public void RestoreBar(Color fullBarColor)
    {
        timer = 0f;
        barImage.fillAmount = 1f;
        //barImage.color = okColor;
        isRunning = false;
        SetBarColor(fullBarColor);  
        SetBarWidth(barImageOriginalWidth);
    }

    private void SetBarColor(Color color)
    {
        barImage.color= color;
    }
    
    public void OnTrickPerformed(Component sender, object data) //Se llama al inicio de los keepTricks y al final
    {       
        if(data is Trick)
        {
            Trick trick = (Trick)data;

            if (trick.isKeepTrick)
            {
                RestoreBar(keepColor);                
                StartKeepIndicator();    
            }
            else
            {
                StartBar(trick.listenInputTime, nullColor);
                if (isKeepTrickActive)
                {
                    StopKeepIndicator();
                }
            }
           
        }
        if(data is float)
        {
            float listenInputTime = (float)data;
            StartBar(listenInputTime, nullColor);
        }
    }
     
    public void OnComboEnd(Component sender, object data)
    {
        if(data is bool)
        {
            bool reset = (bool)data;
            if(reset)
            {
                RestoreBar(noColor);
                StopKeepIndicator();
            }
        }
        //RestoreBar();
    }

    public void OnPerfectTiming(Component sender, object data)
    {
        if(data is bool)
        {
            data = (bool)data;
            if(data is true)
            {
                SetBarColor(perfectColor);   
                //Debug.Log("EventoRecibidoTrue");
            }            
        }
    } 

    public void OnGreatTiming(Component sender, object data)
    {
        if (data is bool)
        {
            data = (bool)data;
            if (data is true)
            {
                SetBarColor(greatColor);
                //Debug.Log("EventoRecibidoTrue");
            }            
        }
    }

    private void StartKeepIndicator()
    {
        isKeepTrickActive = true;
        EnableIndicator(true);        
    }
    private void StopKeepIndicator()
    {
        isKeepTrickActive = false;
        EnableIndicator(false);
    }
    private void EnableIndicator(bool enable)
    {
        if (movingIndicator != null)
        {
            movingIndicator.gameObject.SetActive(enable);
        }        
    }
   

    private void UpdateMovingIndicator()
    {
        if (movingIndicator == null || indicatorArea == null || trickManager == null) return;

        // Movimiento ping-pong entre 0 y 1
        indicatorNormalizedPosition = trickManager.keepTiming;

        float width = indicatorArea.rect.width;
        float xPos = Mathf.Lerp(-width * 0.5f, width * 0.5f, indicatorNormalizedPosition);

        Vector2 anchoredPos = movingIndicator.anchoredPosition;
        anchoredPos.x = xPos;
        movingIndicator.anchoredPosition = anchoredPos;
    }

    private void SetBarWidth(float widthPercentage)
    {
        if (barImage == null) return;           
        float newWidth = barImageOriginalWidth * widthPercentage;
        barImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, widthPercentage);
        indicatorArea = barImage.rectTransform;
    }

    public void HandleOnWallCharge(Component sender, object data)
    {
        if(data is bool)
        {
            bool isWallCharge = (bool)data;
            if(isWallCharge)
            {
                SetBarWidth(barImageWidthPercentageOnCharge);
            }
            else
            {
                SetBarWidth(barImageOriginalWidth);
            }
        }
    }
}
