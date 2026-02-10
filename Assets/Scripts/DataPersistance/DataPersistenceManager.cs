using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName = "gameData.json";

    private GameData gameData;
    public List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;
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

        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
        Debug.Log("OnSceneLoaded");
    }  
    private void OnSceneUnloaded(Scene scene)
    {
        SaveGame();
        Debug.Log("OnSceneUnloaded");
    }
    public void NewGame()
    {
        this.gameData = new GameData();
    }
    public void LoadGame()
    {
        //Load any saved data from a file using the data handler
        this.gameData = dataHandler.Load();

        //if no data can be loaded, initialize to a new game 
        if (this.gameData == null)
        {
            NewGame();
        }
        //push the loaded data to all other scripts that need it
        foreach(IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }
    public void SaveGame()
    {
        //pass the data to other scripts so they can update it
        foreach(IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);          
        }
        dataHandler.Save(gameData);
        //save the updated data to a file using the data handler
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    public void SaveGameOnGameEvent(Component sender, object data)
    {
        Debug.Log("Saving game data on event: " + sender.name);
        SaveGame();
    }    

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistence>();        
        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    public void DeleteSaveData()
    {
        dataHandler.Delete();
        NewGame();
    }
}
