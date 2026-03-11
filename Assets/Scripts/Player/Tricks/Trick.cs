using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spit/Trick")]
public class Trick : ScriptableObject
{
    public int id;
    public AnimationClip animationClip;
    public string trickName;
    public int baseScore;
    public float multiplier = 0.5f;
    public List<Trick> comboTricks = new List<Trick>();
    public KeyCode inputKey;

    public float listenInputTime=1f;
    public bool comboEnder=false;

    //SHOP
    public int cost;    
}
