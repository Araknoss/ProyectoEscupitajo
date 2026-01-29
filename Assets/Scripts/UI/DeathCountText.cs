using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathCountText : MonoBehaviour, IDataPersistence
{
    private TextMeshProUGUI deathText;
    private int deathCount = 0;

    private void Awake()
    {
        deathText = GetComponent<TextMeshProUGUI>();
    }
    public void UpdateDeathCountText(Component sender, object data)
    {
        TMPro.TextMeshProUGUI deathCountText = GetComponent<TMPro.TextMeshProUGUI>();
        deathCountText.text = "Deaths: " + deathCount.ToString();
    }
    public void LoadData(GameData data)
    {
        deathText.text = "Deaths: " + data.deathCount.ToString();
    }
    public void SaveData(ref GameData data)
    {
        //No need to save anything here
    }
}
