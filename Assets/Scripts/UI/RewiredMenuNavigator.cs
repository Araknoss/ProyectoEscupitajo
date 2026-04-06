using UnityEngine;
using UnityEngine.EventSystems;
using Rewired;

public class RewiredMenuNavigator : MonoBehaviour
{
    public enum MenuMode
    {
        MainMenu,
        InGameWithPause
    }

    [Header("General")]
    [SerializeField] private int playerId = 0;
    [SerializeField] private MenuMode menuMode = MenuMode.InGameWithPause;

    [Header("Panels")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject pauseMenuPanel;

    [Header("First Selected")]
    [SerializeField] private GameObject mainMenuFirstSelected;
    [SerializeField] private GameObject pauseMenuFirstSelected;

    [Header("Map Categories")]
    [SerializeField] private string gameplayMapCategory = "Gameplay";
    [SerializeField] private string uiMapCategory = "UI";

    [Header("Actions")]
    [SerializeField] private string pauseAction = "Pause";
    [SerializeField] private string cancelAction = "UICancel";

    private Player player;
    private bool isPaused;

    private void Start()
    {
        if (!ReInput.isReady)
        {
            Debug.LogError("ReInput no está listo.");
            return;
        }

        player = ReInput.players.GetPlayer(playerId);

        if (menuMode == MenuMode.MainMenu)
        {
            OpenMainMenu();
        }
        else
        {
            ClosePauseMenuInstant();
        }
    }

    private void Update()
    {
        //if (!ReInput.isReady || player == null) return;

        //if (menuMode == MenuMode.InGameWithPause)
        //{
        //    if (player.GetButtonDown(pauseAction))
        //    {
        //        if (isPaused) ResumeGame();
        //        else PauseGame();
        //    }

        //    if (isPaused && player.GetButtonDown(cancelAction))
        //    {
        //        ResumeGame();
        //    }

        //    KeepSelectionAlive();
        //}
        //else if (menuMode == MenuMode.MainMenu)
        //{
        //    KeepSelectionAlive();
        //}
    }

    public void OpenMainMenu()
    {
        EnableGameplayMaps(false);
        EnableUiMaps(true);

        if (mainMenuPanel != null) mainMenuPanel.SetActive(true);
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);

        SelectObject(mainMenuFirstSelected);
        isPaused = false;
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;

        EnableGameplayMaps(false);
        EnableUiMaps(true);

        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(true);

        SelectObject(pauseMenuFirstSelected);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;

        EnableUiMaps(false);
        EnableGameplayMaps(true);

        ClearSelection();

        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);
    }

    public void ClosePauseMenuInstant()
    {
        isPaused = false;
        Time.timeScale = 1f;

        EnableUiMaps(false);
        EnableGameplayMaps(true);

        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);
        ClearSelection();
    }

    private void EnableGameplayMaps(bool enabled)
    {
        if (player == null) return;
        player.controllers.maps.SetMapsEnabled(enabled, gameplayMapCategory);
        Debug.Log("Gameplay maps " + (enabled ? "enabled" : "disabled"));
    }

    private void EnableUiMaps(bool enabled)
    {
        if (player == null) return;
        player.controllers.maps.SetMapsEnabled(enabled, uiMapCategory);
        Debug.Log("UI maps " + (enabled ? "enabled" : "disabled"));
    }

    private void SelectObject(GameObject target)
    {
        if (EventSystem.current == null || target == null) return;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(target);
    }

    private void ClearSelection()
    {
        if (EventSystem.current == null) return;
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void KeepSelectionAlive()
    {
        if (EventSystem.current == null) return;

        if (menuMode == MenuMode.MainMenu)
        {
            if (mainMenuPanel != null &&
                mainMenuPanel.activeInHierarchy &&
                EventSystem.current.currentSelectedGameObject == null)
            {
                SelectObject(mainMenuFirstSelected);
            }
        }
        else
        {
            if (isPaused &&
                pauseMenuPanel != null &&
                pauseMenuPanel.activeInHierarchy &&
                EventSystem.current.currentSelectedGameObject == null)
            {
                SelectObject(pauseMenuFirstSelected);
            }
        }
    }
}
