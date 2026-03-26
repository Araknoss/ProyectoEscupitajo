using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateTextOnGameEvent : MonoBehaviour
{
    private TextMeshProUGUI targetText;
    [SerializeField] private float returnAnimationSpeed=5f;
    [SerializeField] private string prefix; 
    void Awake()
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
               targetText.text = data.ToString() + prefix;
               transform.localScale = Vector3.one * 1.2f;
            }
            else if(data is List<Trick>) //Para los trucos disponibles
            {
                List<Trick> tricks = (List<Trick>)data;

                List<string> lines = new List<string>(tricks.Count);
                foreach (Trick t in tricks)
                {
                    if(t.inputKey != KeyCode.None)
                    {
                        lines.Add(t.trickName + " " + t.inputKey.ToString());
                    }
                    else
                    {
                        lines.Add(t.trickName);
                    }

                }

                targetText.text = string.Join("\n", lines); //Unir los nombres de los trucos con saltos de línea y actualizar el texto
            }            
        }
    }    
}
