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
    [SerializeField] private int playerId=0;
    private ControllerType currentControllerType;

    void Start()
    {
        if (pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
        }
        rewiredPlayer = ReInput.players.GetPlayer(playerId);
        GetActiveControllerType();

        rewiredPlayer.controllers.AddLastActiveControllerChangedDelegate(OnLastActiveControllerChanged);
    }

    private void OnDestroy()
    {
        rewiredPlayer.controllers.RemoveLastActiveControllerChangedDelegate(OnLastActiveControllerChanged);
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

    private void OnLastActiveControllerChanged(Player player, Controller controller)
    {
        currentControllerType = controller.type;
        if (currentControllerType == ControllerType.Joystick)
        {
            SetFirstSelected();
            Debug.Log("Controller changed to Joystick");
        }        
    }

    void SetFirstSelected()
    {
        if (pauseMenu.activeSelf)
        {
            EventSystem.current.SetSelectedGameObject(firstSelectedButton.gameObject);
        }
    }

    void GetActiveControllerType()
    {
        //currentControllerType = rewiredPlayer.controllers.GetLastActiveController().type;
    }

}
