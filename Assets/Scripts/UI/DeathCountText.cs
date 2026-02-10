using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathCountText : MonoBehaviour, IDataPersistence
{
    private TextMeshProUGUI deathCountText;
    public int deathCount = 0;

    private void Start()
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
        //UpdateText();
    }
    public void SaveData(ref GameData data)
    {
        data.deathCount = this.deathCount;
        Debug.Log("Saved death count: " + data.deathCount);
    }

    private void UpdateText()
    {        
        deathCountText.text = "Deaths: " + deathCount.ToString();
    }
}
