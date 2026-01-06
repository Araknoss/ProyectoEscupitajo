using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateTextOnGameEvent : MonoBehaviour
{
    private TextMeshProUGUI targetText;
    [SerializeField] private float returnAnimationSpeed=5f;
    void Start()
    {
        targetText = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        transform.localScale= Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime * returnAnimationSpeed);
    }
    public void UpdateScoreText(Component sender, object data)
    {
        if (targetText != null)
        {
            if(data is int) //Para la score
            {
               targetText.text = data.ToString();
               transform.localScale = Vector3.one * 1.2f;
            }
            else if(data is List<Trick>) //Para los trucos disponibles
            {
                List<Trick> tricks = (List<Trick>)data;               

                List<string> trickNames = new List<string>();         // Crear una lista para almacenar los nombres de los trucos disponibles
                foreach (Trick availableTrick in tricks)
                {
                    trickNames.Add(availableTrick.trickName);
                }

                targetText.text = string.Join("\n", trickNames); //Unir los nombres de los trucos con saltos de línea y actualizar el texto
            }
        }
    }    
}
