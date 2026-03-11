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

    [Header("Multiplier")]
    [SerializeField] private float scoreMultiplier = 1f;
    [SerializeField] private float perfectTimingMultiplier = 0.5f;
    public GameEvent onMultiplierUpdate;


    [Header("Gold")]
    public int gold;
    public GameEvent onGoldUpdate;
    [SerializeField] private int goldConversion; 

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
        score = Mathf.Clamp(points,0,points);
        onScoreUpdate.Raise(this, score);
    }

    private void AddMultiplier(float multiplier)
    {
        scoreMultiplier += multiplier;
        onMultiplierUpdate.Raise(this, scoreMultiplier);
    }

    public void HandleTrickPerformed(Component sender, object data)
    {
        if (data is Trick)
        {
            Trick trick = (Trick)data;
            AddScore(trick.baseScore);
            AddMultiplier(trick.multiplier);
        } 
        else if (data is bool) //Cuando se lanza el evento de timing perfecto
        {
            bool onPerfectTiming = (bool)data;
            if (onPerfectTiming)
            {
                AddMultiplier(perfectTimingMultiplier);
            }
        }
    }
    
    public void HandleComboEnd(Component sender, object data)
    {
        //Reset multiplier when combo ends
        scoreMultiplier = 1f;
        onMultiplierUpdate.Raise(this, scoreMultiplier);
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

    public void OnPlayerDeath(Component sender, object data)
    {
        StartCoroutine(ScoreToGoldCo());
    }

    IEnumerator ScoreToGoldCo()
    {
        /*int goldEarned = score / goldConversion; */// Ejemplo: cada 10 puntos de score se convierte en 1 de oro
        int scoreToConvert = score;        
        while (scoreToConvert > 0)
        {
            AddGold(1);
            scoreToConvert -= goldConversion;
            SetScore(scoreToConvert);
            yield return new WaitForSecondsRealtime(0.1f); // Pequeña pausa para el efecto visual
        }
        yield return null;
    }
    public void LoadData(GameData data)
    {        
        this.gold = data.gold;
        onScoreUpdate.Raise(this, score);
        onGoldUpdate.Raise(this, gold);
        Debug.Log("Loaded gold: " + data.gold);
    }

    public void SaveData(/*ref*/ GameData data)
    {
        data.gold = this.gold;
        Debug.Log("Saved gold: " + data.gold);
    }

}
