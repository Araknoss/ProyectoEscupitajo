using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private int playSceneIndex = 1;

    [Header("Menu Buttons")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueGameButton;
    [SerializeField] private Button deleteDataButton;
 
    private void Start()
    {
        if(!DataPersistenceManager.Instance.HasGameData())
        {
            continueGameButton.interactable = false;
        }
    }
    public void OnNewGameClicked()
    {
        DisableMenuButtons();
        DataPersistenceManager.Instance.NewGame();
        SceneManager.LoadSceneAsync(playSceneIndex);
    } 

    public void OnContinueGameClicked()
    {
        DisableMenuButtons();
        //OnSceneLoaded will automatically load the saved data when the play scene is loaded, so we just need to load the scene here
        SceneManager.LoadSceneAsync(playSceneIndex);
    }

    public void OnDeleteDataClicked()
    {
        DataPersistenceManager.Instance.DeleteSaveData();
        continueGameButton.interactable = false;
    }

    public void Quit()
    {        
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }

    private void DisableMenuButtons()
    {
        newGameButton.interactable = false;
        continueGameButton.interactable = false;
        deleteDataButton.interactable = false;
    }   
}
