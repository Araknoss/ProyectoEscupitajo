using MoreMountains.Feedbacks;
using Rewired;
using Rewired.Glyphs.UnityUI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore;
using UnityEngine.UI;

public class UpdateAvailableTricksTextOnGameEvent : MonoBehaviour, IFeedback
{
    [SerializeField] private List<GameObject> childrenGameObjects = new List<GameObject>();
    [SerializeField] private List<MMF_Player> feedbacks = new List<MMF_Player>();
    [SerializeField] private List<TextMeshProUGUI> trickNames = new List<TextMeshProUGUI>();
    [SerializeField] private List<Image> trickBackgrounds = new List<Image>();

    [SerializeField] private Color keepTrickColor;
    [SerializeField] private Color trickColor;

    [Header("Glyphs")]
    [SerializeField] private List<UnityUIPlayerControllerElementGlyph> glyphs = new List<UnityUIPlayerControllerElementGlyph>();

    public void PlayFeedback()
    {
        for (int i = 0; i < feedbacks.Count; i++)
        {
            feedbacks[i]?.PlayFeedbacks();
        }
    }

    public void UpdateText(Component sender, object data)
    {
        for (int i = 0; i < childrenGameObjects.Count; i++)
        {
            if (childrenGameObjects[i] != null)
                childrenGameObjects[i].SetActive(false);
        }

        if (!(data is List<Trick> availableTricks) || availableTricks == null || availableTricks.Count == 0)
        {
            return;
        }

        int max = Mathf.Min(
            availableTricks.Count,
            childrenGameObjects.Count,
            trickNames.Count,
            trickBackgrounds.Count,
            glyphs.Count,
            feedbacks.Count);

        for (int i = 0; i < max; i++)
        {
            var trick = availableTricks[i];
            var child = childrenGameObjects[i];
            if (child != null)
                child.SetActive(true);

            var nameLabel = trickNames[i];
            if (nameLabel != null)
                nameLabel.text = trick?.trickName ?? string.Empty;

            var glyph = glyphs[i];
            if (glyph != null)
                ChangeGlyphById(trick.rewiredActionId, glyph);

            var fb = feedbacks[i];
            fb?.PlayFeedbacks();

            var bg = trickBackgrounds[i];
            if (bg != null)
                bg.color = (trick != null && trick.isKeepTrick) ? keepTrickColor : trickColor;
        }

        for (int i = max; i < childrenGameObjects.Count; i++)
        {
            if (childrenGameObjects[i] != null)
                childrenGameObjects[i].SetActive(false);
        }
    }

    public void ChangeGlyphById(int id, UnityUIPlayerControllerElementGlyph glyph)
    {
        if (glyph == null) return;
        glyph.actionId = id;
        ForceRefresh(glyph);
    }

    private void ForceRefresh(UnityUIPlayerControllerElementGlyph glyph)
    {
        if (glyph == null) return;
        glyph.enabled = false;
        glyph.enabled = true;
    }
}
