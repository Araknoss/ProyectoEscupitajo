using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData : MonoBehaviour
{
    public int gold;
    public int maxLevelReached;

    public PlayerData(Player player)
    {
        gold= player.gold;
        maxLevelReached= player.maxLevelReached;

    }
}
