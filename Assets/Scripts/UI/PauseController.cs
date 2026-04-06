using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Rewired;

public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameEvent onGamePaused;

    [SerializeField] private Button firstSelectedButton;   
    
    [SerializeField] private Player rewiredPlayer;
    [SerializeField] private int playerId;

    void Start()
    {
        if (pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
        }       
        rewiredPlayer = ReInput.players.GetPlayer(playerId);
    }    

    void Update()
    {
        if(rewiredPlayer.GetButtonDown("Pause"))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (pauseMenu.activeSelf)
        {            
            pauseMenu.SetActive(false);
            EventSystem.current.SetSelectedGameObject(null);
            onGamePaused.Raise(this, false);
        }
        else
        {          
            pauseMenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(firstSelectedButton.gameObject);
            onGamePaused.Raise(this, true);
        }
    }  
}
