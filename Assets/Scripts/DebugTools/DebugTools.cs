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

    [Header("Camera Size")]
    [SerializeField] private Slider cameraSizeSlider;
    [SerializeField] private TextMeshProUGUI cameraSizeText;
    [SerializeField] private List<GameObject> limits = new List<GameObject>();

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

    public void SetCameraSize()
    {
        Camera.main.orthographicSize = cameraSizeSlider.value;
        cameraSizeText.text = "Camera Size: " + cameraSizeSlider.value.ToString("");
        int limitIndex = (int)cameraSizeSlider.value;
        for(int i=0;i<limits.Count;i++)
        {
            if(i+5== limitIndex)
            {
                limits[i].SetActive(true);
            }
            else
            {
                limits[i].SetActive(false);
            }
        }

    }
}
