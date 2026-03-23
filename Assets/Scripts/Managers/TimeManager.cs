using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private Coroutine freezeCoroutine;
    [SerializeField] private float perfectTimingFreezeDuration = 0.1f;
    private void Awake()
    {
        Time.timeScale = 1f;
    }
    public void SetTime(float timeScale)
    {
        Time.timeScale = timeScale;
    }

    public void HandleOnGamePause(Component sender, object data)
    {
        bool isPaused = (bool)data;
        if (isPaused)
        {
            SetTime(0f);
        }
        else
        {
            SetTime(1f);
        }
    }         
}
