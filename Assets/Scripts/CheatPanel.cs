using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatPanel : MonoBehaviour
{
    [SerializeField] private GameObject cheatPanel;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            cheatPanel.SetActive(!cheatPanel.activeSelf);
        }
        if(Input.GetKeyDown(KeyCode.F6))
        {
            SceneManager.LoadScene("2_Gameplay");
        }
        if(Input.GetKeyDown(KeyCode.F7))
        {
            SceneManager.LoadScene("6_GameplayTest");
        }
    }
}
