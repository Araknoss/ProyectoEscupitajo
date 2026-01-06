using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int score;
    private float updateTimer = 0f;
    [SerializeField] private float updateTime;
    public GameEvent onScoreUpdate;
    void Start()
    {
        SetScore(0);
    }

    void Update()
    {
        HandlePassiveScore();
    }
    private void HandlePassiveScore()
    {
        updateTimer += Time.deltaTime;
        if (updateTimer >= updateTime)
        {
            AddScore(1);
            updateTimer = 0f;
        }
    }
    private void AddScore(int points)
    {       
        score += points;
        onScoreUpdate.Raise(this, score);
    }
    
    private void SetScore(int points)
    {        
        score = points;
        onScoreUpdate.Raise(this, score);
    }

    public void HandleTrickPerformed(Component sender, object data)
    {
        if (data is not Trick) return;

        Trick trick = (Trick)data;
        AddScore(trick.baseScore);
    }



}
