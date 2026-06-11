using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour/*, IDataPersistence*/
{
    [Header("Score")]
    public int score;
    private float updateTimer = 0f;
    [SerializeField] private float updateTime;
    public GameEvent onScoreUpdate;

    [Header("Temporary Score")]
    private int temporaryScore;
    public GameEvent onTemporaryScoreUpdate;

    [Header("Multiplier")]
    [SerializeField] private int multiplierValue = 1;
    //[SerializeField] private float perfectTimingMultiplier = 0.5f;
    public GameEvent onMultiplierUpdate;

    [SerializeField] private int multiplierIndex; //0, 1, 2.... C, B , A, S, SS, SSS
    [SerializeField] private int actualHardness;
    [SerializeField] private int actualHardnessThreshold;
    [SerializeField] private List<int> multiplierHardnessTresholds= new List<int>();
    [SerializeField] private List<int> multiplierValues = new List<int>();


    //[Header("Gold")]
    //public int gold;
    //public GameEvent onGoldUpdate;
    //[SerializeField] private int goldConversion; 

    void Start()
    {        
        InitializeMultiplier();
        SetScore(0);
    }

    void Update()
    {
        HandlePassiveScore();
    }

    private void InitializeMultiplier()
    {
            multiplierIndex = 0;
            actualHardness = 0;

            multiplierValue = multiplierValues[multiplierIndex];
            actualHardnessThreshold = multiplierHardnessTresholds[multiplierIndex];

            onMultiplierUpdate.Raise(this, multiplierValue);
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
    void AddTemporaryScore(int points)
    {
        temporaryScore += points*multiplierValue;  
        onTemporaryScoreUpdate.Raise(this, temporaryScore);
    }
    void ResetTemporaryScore()
    {
        temporaryScore = 0;
    }
    private void AddScore(int points)
    {       
        score += points*multiplierValue;
        onScoreUpdate.Raise(this, score);
    }
    
    private void SetScore(int points)
    {        
        score = Mathf.Clamp(points,0,points);
        onScoreUpdate.Raise(this, score);
    }

    private void AddHardness(int hardness)
    {
        actualHardness += hardness;
        if(actualHardness >= actualHardnessThreshold)
        {
            actualHardness = 0;
            UpdateMultiplier(multiplierIndex+1);
        }
    }
    private void UpdateMultiplier(int index) //Pasamos al siguiente multiplicador, 2x, 4x, 6x...
    {
        //scoreMultiplier += multiplier;        
        multiplierIndex = Mathf.Clamp(index, 0, multiplierValues.Count-1);

        multiplierValue = multiplierValues[multiplierIndex];
        actualHardnessThreshold = multiplierHardnessTresholds[multiplierIndex];

        onMultiplierUpdate.Raise(this, multiplierValue);
    }

    public void HandleTrickPerformed(Component sender, object data)
    {
        if (data is Trick)
        {
            Trick trick = (Trick)data;
            AddTemporaryScore(trick.baseScore);
            AddScore(trick.baseScore);
            AddHardness(trick.hardness);
        } 
        else if (data is bool) //Cuando se lanza el evento de timing perfecto
        {
            bool onPerfectTiming = (bool)data;
            if (onPerfectTiming)
            {
                //AddMultiplier(perfectTimingMultiplier);
            }
        }
    }
    
    public void HandleComboEnd(Component sender, object data)
    {
        //Reset multiplier when combo ends
        if (data is bool)
        {
            bool resetMultiplier = (bool)data;
            if (resetMultiplier)
            {
                UpdateMultiplier(0); //Volvemos al multiplicador base
                ResetTemporaryScore();
                onMultiplierUpdate.Raise(this, multiplierValue);
            }
        }
    }

    //public void Buy(int price)
    //{
    //    gold -= price;
    //    onGoldUpdate.Raise(this, gold);
    //}   

    public void OnPlayerDeath(Component sender, object data)
    {
        if(GoldManager.Instance == null) return;
        GoldManager.Instance.AddGoldFromScore(score);
        //StartCoroutine(ScoreToGoldCo()); //Desactivado para la demo
    }

    //IEnumerator ScoreToGoldCo()
    //{
    //    /*int goldEarned = score / goldConversion; */// Ejemplo: cada 10 puntos de score se convierte en 1 de oro

    //    int scoreToConvert = score;        
    //    while (scoreToConvert > 0)
    //    {
    //        AddGold(1);
    //        scoreToConvert -= goldConversion;
    //        SetScore(scoreToConvert);
    //        yield return new WaitForSecondsRealtime(0.1f); // Pequeña pausa para el efecto visual
    //    }
    //    yield return null;
    //}
    //public void LoadData(GameData data)
    //{        
    //    this.gold = data.gold;
    //    onScoreUpdate.Raise(this, score);
    //    onGoldUpdate.Raise(this, gold);
    //    Debug.Log("Loaded gold: " + data.gold);
    //}

    //public void SaveData(/*ref*/ GameData data)
    //{
    //    data.gold = this.gold;
    //    Debug.Log("Saved gold: " + data.gold);
    //}

}
