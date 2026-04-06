using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateAvailableTricksTextOnGameEvent : MonoBehaviour
{
    [SerializeField] private List<Image> trickImages;
    [SerializeField] private List<Image> images;

    public void UpdateScoreText(Component sender, object data)
    {
        
            foreach(Image image in trickImages)
            {
                 image.sprite = null;
                 image.color=Color.clear;
        }
            foreach(Image img in images)
            {
                img.enabled = false;
            }
        if (data is List<Trick>) //Para los trucos disponibles
            {
                List<Trick> tricks = (List<Trick>)data;                
                for(int i=0; i< tricks.Count; i++)
                {
                    trickImages[i].sprite = tricks[i].sprite;
                    trickImages[i].color = Color.white;
                    images[i].enabled = true;                    
                }
            }       
    }
}
