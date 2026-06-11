using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateScoreTextOnEnable : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI targetText;
    private void OnEnable()
    {        
        ScoreManager scoreManager = FindAnyObjectByType<ScoreManager>();
        if (scoreManager != null)
        {
            targetText.text = "Score: " + scoreManager.score.ToString();
        }
    }
}
