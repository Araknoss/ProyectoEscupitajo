using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockablesManager : MonoBehaviour, IDataPersistence
{
    public SerializableDictionary<int, bool> unlockedTricks = new SerializableDictionary<int, bool>(); // Clave: Trick ID, Valor: Desbloqueado o no
    public static UnlockablesManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }
    public void LoadData(GameData data)
    {
        unlockedTricks = data.unlockedTricks;
    }
    public void SaveData(ref GameData data)
    {
        data.unlockedTricks = unlockedTricks;
    }
    public bool HasUnlockedTrick(Trick trick)
    {
        if(unlockedTricks.ContainsKey(trick.id))
        {
            return true ;
        }
        else
        {
             return false;
        }          
    }
    public void UnlockTrick(Component sender, object data) //Aqui pasamos el id del scriptableObject al diccionario y lo ponemos en true
    {
        if(data is int trickId)
        {
            if (!unlockedTricks.ContainsKey(trickId))
            {
                unlockedTricks[trickId] = true;
                Debug.Log("Trick unlocked: " + trickId);
            }
        }        
    }
}
