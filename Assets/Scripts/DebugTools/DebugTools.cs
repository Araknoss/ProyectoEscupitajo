using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DebugTools : MonoBehaviour
{  
    [Header("Chunk Speed")]
    [SerializeField] private GameEvent onChunkSpeedChanged;
    [SerializeField] private Slider chunkSpeedSlider;
    [SerializeField] private TextMeshProUGUI chunkSpeedText;

    [Header("Player Speed")]
    [SerializeField] private GameEvent onPlayerSpeedChanged;
    [SerializeField] private Slider playerSpeedSlider;
    [SerializeField] private TextMeshProUGUI playerSpeedText;

    public void SetChunkSpeed()
    {
        onChunkSpeedChanged.Raise(this, chunkSpeedSlider.value);
        chunkSpeedText.text = "Chunk Speed: " + chunkSpeedSlider.value.ToString("");
    }

    public void SetPlayerSpeed()
    {
        onPlayerSpeedChanged.Raise(this, playerSpeedSlider.value);
        playerSpeedText.text = "Player Speed: " + playerSpeedSlider.value.ToString("");
    }    
}
