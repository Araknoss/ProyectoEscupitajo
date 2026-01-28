using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistenceManager : MonoBehaviour
{
    private GameData gameData;
    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Multiple instances of DataPersistenceManager detected. Destroying duplicate.");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

    private void Start()
    {
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        //Load any saved data from a file using the data handler
        //if no data can be loaded, initialize to a new game 
        if(this.gameData == null)
        {
            Debug.Log("No data found. Initializing to new game.");
            NewGame();
        }
        //push the loaded data to all other scripts that need it
    }

    public void SaveGame()
    {
        //pass the data to other scripts so they can update it
        //save the updated data to a file using the data handler
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

}
