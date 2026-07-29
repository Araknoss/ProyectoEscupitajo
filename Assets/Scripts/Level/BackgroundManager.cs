using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Cambia el fondo del nivel cuando ChunkManager notifica un cambio de nivel
/// a través del GameEvent onLevelChanged.
/// </summary>
public class BackgroundManager : MonoBehaviour
{
   List<GameObject> backgrounds = new List<GameObject>();
    [SerializeField] private float delayBeforeChange = 0.5f; // Retardo antes de cambiar el fondo
    private void Awake()
    {
        // Desactiva todos los fondos al inicio
        foreach (Transform child in transform)
        {  
            backgrounds.Add(child.gameObject);
        }

    }
    public void OnLevelChanged(Component sender, object data)
    {
        if (data is not int newLevelIndex)
            return;

        StartCoroutine(ChangeBackgroundWithDelay(newLevelIndex));        
    }

    IEnumerator ChangeBackgroundWithDelay(int index)
    {
        yield return new WaitForSeconds(delayBeforeChange);
        foreach (var bg in backgrounds)
        {
            bg.SetActive(false);
        }
        if (index >= 0 && index < backgrounds.Count)
        {
            backgrounds[index].SetActive(true);
        }        
    }
}