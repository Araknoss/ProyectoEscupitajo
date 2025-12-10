using UnityEngine;

[CreateAssetMenu(menuName = "Spit/Trick")]
public class Trick : ScriptableObject
{
    public AnimationClip animationClip;
    public string trickName;
    public int baseScore;
    public float multiplier = 1f;    
}
