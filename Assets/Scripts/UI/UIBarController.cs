using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private Color fullColor = Color.green;
    [SerializeField] private Color emptyColor = Color.red;

    void Update()
    {
        if (!isRunning) return;      

        timer += Time.deltaTime;

        float normalized = 1f - (timer / duration); // 1 -> 0
        normalized = Mathf.Clamp01(normalized);

        HandleBarWidth(normalized);
        HandleBarColor(normalized);        

        if (timer >= duration)
        {
            isRunning = false;
            barImage.fillAmount = 0f;
        }
    }

    public void StartBar(float timeToEmpty)
    {
        Debug.Log("StartBar");
        duration = timeToEmpty;
        timer = 0f;
        isRunning = true;
        barImage.fillAmount = 1f;
    }

    public void RestoreBar()
    {
        timer = 0f;
        barImage.fillAmount = 1f;
        barImage.color = fullColor;
        isRunning = false;
    }
    private void HandleBarWidth(float normalized)
    {
        barImage.fillAmount = normalized;
    }
    private void HandleBarColor(float normalized)
    {       
        barImage.color = Color.Lerp(emptyColor, fullColor, normalized);
    }

    public void OnTrickPerformed(Component sender, object data)
    {       
        if(data is Trick)
        {
            Trick trick = (Trick)data;
            if (trick.id == 10)
            {
                RestoreBar();
                return;
            }
            StartBar(trick.listenInputTime);
        }
    }
}
