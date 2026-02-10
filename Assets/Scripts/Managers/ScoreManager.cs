using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour, IDataPersistence
{
    [Header("Score")]
    private int score;
    private float updateTimer = 0f;
    [SerializeField] private float updateTime;
    public GameEvent onScoreUpdate;

    [Header("Gold")]
    public int gold;
    public GameEvent onGoldUpdate;

    public static ScoreManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persiste entre escenas
        }
        else
        {
            Destroy(gameObject); // Evita duplicados
        }
    }
    void Start()
    {
        SetScore(0);
    }

    void Update()
    {
        HandlePassiveScore();
    }
    private void HandlePassiveScore()
    {
        updateTimer += Time.deltaTime;
        if (updateTimer >= updateTime)
        {
            AddScore(1);
            updateTimer = 0f;
        }
    }
    private void AddScore(int points)
    {       
        score += points;
        onScoreUpdate.Raise(this, score);
    }
    
    private void SetScore(int points)
    {        
        score = points;
        onScoreUpdate.Raise(this, score);
    }

    public void HandleTrickPerformed(Component sender, object data)
    {
        if (data is not Trick) return;

        Trick trick = (Trick)data;
        AddScore(trick.baseScore);
    }

    public void Buy(int price)
    {
        gold -= price;
        onGoldUpdate.Raise(this, gold);
    }

    public void AddGold(int amount)
    {
        gold += amount;
        onGoldUpdate.Raise(this, gold);
    }

    public void OnAddGold(Component sender, object data) //Para el menu principal, usa para añadir oro al hacer click en un botón de recompensa
    {
        if (data is not int) return;
        int amount = (int)data;
        AddGold(amount);
    }   
    
    public void LoadData(GameData data)
    {        
        this.gold = data.gold;
        onScoreUpdate.Raise(this, score);
        onGoldUpdate.Raise(this, gold);
    }

    public void SaveData(GameData data)
    {
        data.gold = this.gold;
    }
}
