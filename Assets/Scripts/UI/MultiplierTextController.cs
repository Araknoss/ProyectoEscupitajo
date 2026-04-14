using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MultiplierTextController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI targetText;
    [SerializeField] private float returnAnimationSpeed = 5f;
    [SerializeField] private string prefix;

    [SerializeField] private GameObject background;
    
    void Awake()
    {
        targetText.alpha = 0f; // Asegura que el texto comience invisible
        background.SetActive(false);
    }
    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime * returnAnimationSpeed);              
    }
    public void UpdateScoreText(Component sender, object data)
    {
        if (targetText != null)
        {
            if (data is int) //Para el multiplier
            {     
                int multiplierValue = (int)data;
                if (multiplierValue == 1)
                {
                    targetText.alpha = 0f;
                    background.SetActive(false); // Ocultar el fondo cuando el multiplicador es 1
                    return; 
                }
                    background.SetActive(true); // Mostrar el fondo para multiplicadores mayores a 1
                    targetText.alpha = 1f; // Hacer el texto visible
                    transform.localScale = Vector3.one * 1.5f; // Aumentar el tamaño del texto
                    string multiplierText = data.ToString() + "x";
                    targetText.text = multiplierText;               
                
            }
        }
    }
}
