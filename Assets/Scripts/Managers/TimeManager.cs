using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 1f;
    }
    public void SetTime(float timeScale)
    {
        Time.timeScale = timeScale;
    }
}
