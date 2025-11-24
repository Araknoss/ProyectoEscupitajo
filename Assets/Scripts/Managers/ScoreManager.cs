using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int score;
    private float updateTimer = 0f;
    [SerializeField] private float updateTime;
    public GameEvent onScoreChanged;
    void Start()
    {
        SetScore(0);
    }

    void Update()
    {
        updateTimer += Time.deltaTime;
        if(updateTimer >= updateTime)
        {
            AddScore(1);
            updateTimer = 0f;
        }
    }

    private void AddScore(int points)
    {       
        score += points;
        onScoreChanged.Raise(this, score);
    }
    
    private void SetScore(int points)
    {        
        score = points;
        onScoreChanged.Raise(this, score);
    }
    


}
