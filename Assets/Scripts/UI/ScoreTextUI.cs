using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreTextUI : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    [SerializeField] private float returnAnimationSpeed=5f;
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        transform.localScale= Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime * returnAnimationSpeed);
    }
    public void UpdateScoreText(Component sender, object data)
    {
        if (scoreText != null)
        {
            scoreText.text = data.ToString();
            transform.localScale = Vector3.one * 1.2f;
        }
    }
}
