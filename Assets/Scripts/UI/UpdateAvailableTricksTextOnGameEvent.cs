using Rewired;
using Rewired.Glyphs.UnityUI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore;
using UnityEngine.UI;

public class UpdateAvailableTricksTextOnGameEvent : MonoBehaviour
{
    [SerializeField] private List<GameObject> childrenGameObjects = new List<GameObject>();
    [SerializeField] private List<TextMeshProUGUI> trickNames=new List<TextMeshProUGUI>();
    [SerializeField] private List<Image> trickBackgrounds = new List<Image>();

    [SerializeField] private Color keepTrickColor;
    [SerializeField] private Color trickColor;

    [Header("Glyphs")]
    [SerializeField] private List<UnityUIPlayerControllerElementGlyph> glyphs = new List<UnityUIPlayerControllerElementGlyph>();   


    private void Update()
    {
        
    }
    public void UpdateScoreText(Component sender, object data)
    {         
            foreach(GameObject child in childrenGameObjects)
            {
                child.SetActive(false);
             }

        if (data is List<Trick>) //Para los trucos disponibles
            {
            List<Trick> availableTricks = (List<Trick>)data;
            for (int i = 0; i < availableTricks.Count; i++)
            {
                childrenGameObjects[i].SetActive(true);
                trickNames[i].text = availableTricks[i].trickName;
                ChangeGlyphById(availableTricks[i].rewiredActionId, glyphs[i]);
                if (availableTricks[i].isKeepTrick)
                {
                    trickBackgrounds[i].color = keepTrickColor;
                }
                else
                {
                    trickBackgrounds[i].color = trickColor;
                }
               
            }       
    }
}
    

    public void ChangeGlyphById(int id, UnityUIPlayerControllerElementGlyph glyph)
    {       
        glyph.actionId = id;
        ForceRefresh(glyph);
    }

    private void ForceRefresh(UnityUIPlayerControllerElementGlyph glyph)
    {        
        glyph.enabled = false;
        glyph.enabled = true;
    }
}
