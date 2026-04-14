using UnityEngine;
using Rewired;
using UnityEngine.SceneManagement;

public class RewiredMapSwitcher : MonoBehaviour
{
    [SerializeField] private int mainMenuSceneIndex = 0;
    [SerializeField] private int playerId = 0;
    public string currentMapName = "UI";

    private Player player;

    private void Awake()
    {
        player = ReInput.players.GetPlayer(playerId);
    }

    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex== mainMenuSceneIndex)
        {
            EnableUI();
        }
        else
        {
            EnableGameplay();
        }
    }

    public void EnableGameplay()
    {
        player.controllers.maps.SetMapsEnabled(false, "UI");
        player.controllers.maps.SetMapsEnabled(true, "Gameplay");
        currentMapName = "Gameplay";        
    }

    public void EnableUI()
    {
        player.controllers.maps.SetMapsEnabled(false, "Gameplay");
        player.controllers.maps.SetMapsEnabled(true, "UI");
        currentMapName = "UI";       
    }

    public void OnGamePause(Component sender, object data)
    {
        if(data is bool isPaused)
        {
            if (isPaused)
            {
                EnableUI();
            }
            else
            {
                EnableGameplay();
            }
        }
    }
}
