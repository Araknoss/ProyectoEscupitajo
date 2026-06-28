using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManager : MonoBehaviour, IDataPersistence
{
    public static GoldManager Instance;

    public int gold;
    public GameEvent onGoldUpdate;
    [SerializeField] private int goldConversion = 100;
    public int goldFromScore; 

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

        onGoldUpdate.Raise(this, gold);
    }
    public void AddGold(int amount)
    {
        gold += amount;
        onGoldUpdate.Raise(this, gold);
    }
    public void AddGoldFromScore(int score)
    {
        int goldToAdd = score / goldConversion;
        Debug.Log("Adding " + goldToAdd + " gold from score: " + score);

        goldFromScore = goldToAdd; //Para mostrarlo en UI
        AddGold(goldToAdd);
    }
    public void Buy(int price)
    {
        gold -= price;
        onGoldUpdate.Raise(this, gold);
    }
    public void OnAddGold(Component sender, object data) //Para el menu principal, usa para añadir oro al hacer click en un botón de recompensa
    {
        if (data is not int) return;
        int amount = (int)data;
        AddGold(amount);
    }

//#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F4))
        {
            AddGold(100);
        }
    }
//#endif

    public void LoadData(GameData data)
    {
        gold = data.gold;
        onGoldUpdate.Raise(this, gold);
        Debug.Log("Gold loaded: " + gold);
    }
    public void SaveData(GameData data)
    {
        data.gold = gold;
        Debug.Log("Gold saved: " + gold);
    }
}
