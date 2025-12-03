using UnityEngine;

[CreateAssetMenu(menuName = "Spit/Trick")]
public class Trick : ScriptableObject
{
    public AnimationClip trickAnimation;
    public string trickName;
    public int baseScore;
    public float difficultyMultiplier = 1f;
    public int extraMultiplier = 0;
}
