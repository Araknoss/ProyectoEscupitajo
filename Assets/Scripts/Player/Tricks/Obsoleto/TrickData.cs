using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTrick", menuName = "Trick")]
public class TrickData : ScriptableObject
{
    [Header("Info")]
    public string trickName = "New Trick";

    [Header("Scoring Data (used by Score system, not by TrickManager)")]
    public int baseScore = 100;
    public float scoreMultiplier = 1f;

    [Header("Animation Data (used by animation system)")]
    public AnimationClip trickAnimation;

    [Header("Combo Rules")]
    [Tooltip("If true, this trick can start a combo with no previous trick.")]
    public bool canStartCombo = true;

    [Tooltip("If not empty, this trick ONLY can be used after one of these tricks.")]
    public List<TrickData> allowedPreviousTricks = new List<TrickData>();
}
