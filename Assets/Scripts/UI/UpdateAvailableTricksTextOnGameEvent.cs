using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

public class UpdateAvailableTricksTextOnGameEvent : MonoBehaviour
{
    [SerializeField] private List<Image> trickImages;
    [SerializeField] private List<Image> inputImages;

    [SerializeField] private Sprite bodyInputImage;
    [SerializeField] private Sprite skateInputImage;
    [SerializeField] private Sprite keepInputImage;

    [SerializeField] private Sprite bodyInputImageController;
    [SerializeField] private Sprite skateInputImageController;
    [SerializeField] private Sprite keepInputImageController;

    private void Update()
    {
        
    }
    public void UpdateScoreText(Component sender, object data)
    {        
            foreach(Image image in trickImages)
            {
                 image.sprite = null;
                 image.color=Color.clear;
        }
            foreach(Image img in inputImages)
            {
                img.enabled = false;
            }
        if (data is List<Trick>) //Para los trucos disponibles
            {
            List<Trick> tricks = (List<Trick>)data;
            for (int i = 0; i < tricks.Count; i++)
            {
                //trickImages[i].sprite = tricks[i].sprite;
                //trickImages[i].color = Color.white;
                //inputImages[i].enabled = true;

                //if(InputDeviceDetector.Instance.CurrentInput == InputDeviceDetector.InputType.Controller)
                //{
                //    if (tricks[i].rewiredActionId == 2)
                //    {

                //        inputImages[i].sprite = bodyInputImageController;
                //    }
                //    else if (tricks[i].rewiredActionId == 3)
                //    {
                //        inputImages[i].sprite = skateInputImageController;  
                //    }
                //    else if (tricks[i].rewiredActionId == 4)
                //    {
                //        inputImages[i].sprite = keepInputImageController;
                //    }
                //    else
                //    {
                //        inputImages[i].sprite = null;
                //    }
                //}
                //else if(InputDeviceDetector.Instance.CurrentInput == InputDeviceDetector.InputType.KeyboardMouse)
                //{
                //    if (tricks[i].rewiredActionId == 2)
                //    {
                //        inputImages[i].sprite = bodyInputImage;
                //    }
                //    else if (tricks[i].rewiredActionId == 3)
                //    {
                //        inputImages[i].sprite = skateInputImage;  
                //    }
                //    else if (tricks[i].rewiredActionId == 4)
                //    {
                //        inputImages[i].sprite = keepInputImage;
                //    }
                //    else
                //    {
                //        inputImages[i].sprite = null;
                //    }
                //}
        }       
    }
}
}
