using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int deathCount;
    public Dictionary<int, bool> unlockedTricks = new Dictionary<int, bool>();

    //Initial values when starting a new game
    public GameData()
    {
        this.deathCount = 0;
        this.unlockedTricks = new Dictionary<int, bool>();
    }
}
