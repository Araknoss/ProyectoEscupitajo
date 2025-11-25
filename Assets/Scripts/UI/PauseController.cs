using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameEvent onGamePaused;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        if (pauseMenu.activeSelf)
        {            
            pauseMenu.SetActive(false);
            onGamePaused.Raise(this, false);
        }
        else
        {          
            pauseMenu.SetActive(true);
            onGamePaused.Raise(this, true);
        }
    }
}
