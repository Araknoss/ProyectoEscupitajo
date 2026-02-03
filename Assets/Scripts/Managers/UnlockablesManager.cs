using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockablesManager : MonoBehaviour, IDataPersistence
{
    public Dictionary<int, bool> unlockedTricks = new Dictionary<int, bool>(); // Clave: Trick ID, Valor: Desbloqueado o no
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
        // Implementa la lógica para verificar si el truco está desbloqueado
        return true; // Placeholder
    }
    public void UnlockTrick(Trick trick)
    {
        if (!unlockedTricks.ContainsKey(trick.id))
        {
            unlockedTricks[trick.id] = true;
        }
    }
}
