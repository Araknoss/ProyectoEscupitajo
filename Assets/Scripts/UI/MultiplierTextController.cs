using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MultiplierTextController : MonoBehaviour
{
    private TextMeshProUGUI targetText;
    [SerializeField] private float returnAnimationSpeed = 5f;
    [SerializeField] private string prefix;
    
    void Awake()
    {
        targetText = GetComponent<TextMeshProUGUI>();
        targetText.alpha = 0f; // Inicialmente el texto es invisible
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
                
                    targetText.alpha = 1f; // Hacer el texto visible
                    transform.localScale = Vector3.one * 1.2f; // Aumentar el tamaño del texto
                    string multiplierText = data.ToString() + "x";
                    targetText.text = multiplierText;               
                
            }
        }
    }
}
