using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathCountText : MonoBehaviour, IDataPersistence
{
    private TextMeshProUGUI deathCountText;
    private int deathCount = 0;

    private void Awake()
    {
        deathCountText = GetComponent<TextMeshProUGUI>();
        UpdateText();
    }
    public void OnPlayerDeath(Component sender, object data)
    {
        deathCount++;
        UpdateText();        
    }
    public void LoadData(GameData data)
    {
        this.deathCount= data.deathCount;
        deathCountText.text = "Deaths: " + data.deathCount.ToString();
    }
    public void SaveData(ref GameData data)
    {
        data.deathCount = this.deathCount;
        //No need to save anything here
    }

    private void UpdateText()
    {        
        deathCountText.text = "Deaths: " + deathCount.ToString();
    }
}
