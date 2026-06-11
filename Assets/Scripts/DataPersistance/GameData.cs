using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int deathCount;
    public SerializableDictionary<int, bool> unlockedTricks = new SerializableDictionary<int, bool>();
    public int gold;
    public bool tutorialCompleted;

    //Initial values when starting a new game
    public GameData()
    {
        this.deathCount = 0;
        this.unlockedTricks = new SerializableDictionary<int, bool>();
        this.gold = 0;
        this.tutorialCompleted = false;
    }
}
