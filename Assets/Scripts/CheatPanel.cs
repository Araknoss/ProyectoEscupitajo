using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatPanel : MonoBehaviour
{
    [SerializeField] private GameObject cheatPanel;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            cheatPanel.SetActive(!cheatPanel.activeSelf);
        }
    }
}
