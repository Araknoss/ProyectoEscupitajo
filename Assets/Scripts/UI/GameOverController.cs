using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject firstSelectedButton;

    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private string tryAgainText = "Game Over! Try Again?";
    [SerializeField] private string victoryText = "You win!";

    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private TextMeshProUGUI scoreText;
    public void OnPlayerDeath(Component sender, object data)
    {
        gameOverText.text = tryAgainText;   
        gameOverPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(firstSelectedButton);
        scoreText.text = scoreManager.score.ToString();
    }
    public void OnPlayerVictory(Component sender, object data)
    {
        gameOverText.text = victoryText;
        gameOverPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(firstSelectedButton);
        scoreText.text = scoreManager.score.ToString();
    }
}
