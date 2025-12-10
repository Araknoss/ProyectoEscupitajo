using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    [Header("Combo Settings")]
    [SerializeField] private float comboTimeout = 1.5f;
    [SerializeField] private int baseMultiplier = 1;
    [SerializeField] private float repeatPenalty = 0.5f;

    [Header("Score")]
    [SerializeField] private int totalScore;
    [SerializeField] private int currentComboScore;
    [SerializeField] private int currentMultiplier;
    [SerializeField] private bool comboActive;

    [Header("Debug")]
    [SerializeField] private float comboTimer;
    [SerializeField] private List<string> tricksInCurrentCombo = new List<string>();

    private void Awake()
    {
        currentMultiplier = baseMultiplier;
    }

    private void Update()
    {
        HandleComboTimer();
    }

    private void HandleComboTimer()
    {
        if (!comboActive)
        {
            return;
        }

        comboTimer -= Time.deltaTime;

        if (comboTimer <= 0f)
        {
            ConfirmCombo();
        }
    }

    public void StartCombo()
    {
        if (comboActive)
        {
            return;
        }

        comboActive = true;
        comboTimer = comboTimeout;
        currentComboScore = 0;
        currentMultiplier = baseMultiplier;
        tricksInCurrentCombo.Clear();
    }

    public void AddTrick(Trick trick)
    {
        if (trick == null)
        {
            return;
        }

        if (!comboActive)
        {
            StartCombo();
        }

        comboTimer = comboTimeout;

        bool isRepeated = tricksInCurrentCombo.Contains(trick.trickName);
        tricksInCurrentCombo.Add(trick.trickName);

        int trickScore = trick.baseScore;

        if (isRepeated)
        {
            trickScore = Mathf.RoundToInt(trickScore * repeatPenalty);
        }

        trickScore = Mathf.RoundToInt(trickScore /** trick.difficultyMultiplier*/);

        //currentMultiplier += trick.extraMultiplier;

        trickScore *= currentMultiplier;

        currentComboScore += trickScore;
    }

    public void ConfirmCombo()
    {
        if (!comboActive)
        {
            return;
        }

        totalScore += currentComboScore;
        EndCombo();
    }

    public void EndCombo()
    {
        comboActive = false;
        currentComboScore = 0;
        currentMultiplier = baseMultiplier;
        tricksInCurrentCombo.Clear();
        comboTimer = 0f;
    }

    public int GetTotalScore()
    {
        return totalScore;
    }

    public int GetCurrentComboScore()
    {
        return currentComboScore;
    }

    public bool IsComboActive()
    {
        return comboActive;
    }
}

