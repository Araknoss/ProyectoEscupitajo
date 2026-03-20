using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIBarController : MonoBehaviour
{
        [SerializeField] private Image barImage;
        [SerializeField] private float decreaseSpeed = 0.2f;
    private float timer;
    private bool isRunning;
    private float duration;

        private float currentValue = 1f;

    [Header("Color")]
    [SerializeField] private Color greatColor = Color.green;
    [SerializeField] private Color perfectColor = Color.red;
    [SerializeField] private Color nullColor = Color.gray;

    void Update()
    {
        if (!isRunning) return;

        timer += Time.deltaTime;

        float normalized = 1f - (timer / duration); // 1 -> 0
        normalized = Mathf.Clamp01(normalized);

        // Tamaño (relleno)
        barImage.fillAmount = normalized;

        // Color (a menos valor, más rojo)
        //barImage.color = Color.Lerp(emptyColor, fullColor, normalized);

        if (timer >= duration)
        {
            isRunning = false;
            barImage.fillAmount = 0f;
            //barImage.color = emptyColor;
        }
    }

    public void StartBar(float timeToEmpty)
    {
        //Debug.Log("StartBar");
        duration = timeToEmpty;
        timer = 0f;
        isRunning = true;
        barImage.fillAmount = 1f;
        SetBarColor(nullColor);
    }

    public void RestoreBar()
    {
        timer = 0f;
        barImage.fillAmount = 1f;
        //barImage.color = okColor;
        isRunning = false;
        SetBarColor(nullColor);  
    }

    private void SetBarColor(Color color)
    {
        barImage.color= color;
    }
    private void HandleBarWidth(float normalized)
    {
        barImage.fillAmount = normalized;
    }
    //private void HandleBarColor(float normalized)
    //{       
    //    barImage.color = Color.Lerp(emptyColor, fullColor, normalized);
    //}

    public void OnTrickPerformed(Component sender, object data)
    {       
        if(data is Trick)
        {
            Trick trick = (Trick)data;            
            StartBar(trick.listenInputTime);
        }
        if(data is float)
        {
            float listenInputTime = (float)data;
            StartBar(listenInputTime);
        }
    }

    public void OnWallSlidePerformed(Component sender, object data)
    {
        //if (data is Trick)
        //{
        //    RestoreBar();
        //}
    }

    public void OnKeepTrickPerfomed(Component sennder, object data)
    {
        //if(data is Trick)
        //{
        //    RestoreBar();
        //}

    }

    public void OnComboEnd(Component sender, object data)
    {
        if(data is bool)
        {
            bool reset = (bool)data;
            if(reset)
            {
                RestoreBar();
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
}
