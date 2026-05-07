using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "NewGlyphSet",
    menuName = "Input/Glyph Set"
)]
public class GlyphSet : ScriptableObject
{
    [SerializeField] private string glyphSetName;
    [SerializeField] private List<ActionGlyph> glyphs = new List<ActionGlyph>();

    public string GlyphSetName => glyphSetName;

    public Sprite GetGlyph(string actionName)
    {
        foreach (ActionGlyph glyph in glyphs)
        {
            if (glyph.actionName == actionName)
            {
                return glyph.sprite;
            }
        }

        return null;
    }
}
