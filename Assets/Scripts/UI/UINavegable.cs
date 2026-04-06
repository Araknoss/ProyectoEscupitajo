using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UINavegable : MonoBehaviour
{
    [SerializeField] private Button firstSelectedButton;

    [SerializeField] private Player rewiredPlayer;
    [SerializeField] private int playerId = 0;
    private ControllerType currentControllerType;
    private void Start()
    {
        rewiredPlayer = ReInput.players.GetPlayer(playerId);
        //GetActiveControllerType();
        rewiredPlayer.controllers.AddLastActiveControllerChangedDelegate(OnLastActiveControllerChanged);
    }
    private void OnDestroy()
    {
        rewiredPlayer.controllers.RemoveLastActiveControllerChangedDelegate(OnLastActiveControllerChanged);
    }
    private void OnLastActiveControllerChanged(Player player, Controller controller)
    {
        Debug.Log("Last active controller changed: " + controller.name);
        currentControllerType = controller.type;
        if (currentControllerType == ControllerType.Joystick)
        {
            SetButtonSelected(firstSelectedButton);
            Debug.Log("Controller changed to Joystick");
        }
    }  

    void SetButtonSelected(Button buttonSelected)
    {        
       EventSystem.current.SetSelectedGameObject(buttonSelected.gameObject);      
    }

    void GetActiveControllerType()
    {
        currentControllerType = rewiredPlayer.controllers.GetLastActiveController().type;
    }
}
