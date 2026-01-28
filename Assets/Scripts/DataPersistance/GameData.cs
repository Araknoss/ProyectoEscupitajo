using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
     public int deathCount;

    //Initial values when starting a new game
    public GameData()
    {
        this.deathCount = 0;
    }
}
