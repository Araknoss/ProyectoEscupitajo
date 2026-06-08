using Rewired.Glyphs.UnityUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TutorialButtonFeedbackOnPressed : MonoBehaviour
{
    [SerializeField] private UnityUIPlayerControllerElementGlyph buttonGlyph;
    public void OnButtonPressed(Component sender, object data)
    {
       if(data is string actionName)
        {
            if(actionName == buttonGlyph.actionName)
            {
                gameObject.SetActive(false);
            }            
        }
    }
}
