using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public static GoldManager Instance;

    public int gold;
    public GameEvent onGoldUpdate;
    [SerializeField] private int goldConversion;

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

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            AddGold(100);
        }
    }
#endif
}
