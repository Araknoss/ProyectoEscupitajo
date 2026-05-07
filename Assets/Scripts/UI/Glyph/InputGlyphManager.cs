using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class InputGlyphManager : MonoBehaviour
{
    [Header("Rewired")]
    [SerializeField] private int playerId = 0;

    [Header("Glyph Sets")]
    [SerializeField] private GlyphSet keyboardGlyphSet;
    [SerializeField] private GlyphSet xboxGlyphSet;
    [SerializeField] private GlyphSet playStationGlyphSet;
    [SerializeField] private GlyphSet genericGamepadGlyphSet;

    [Header("UI Glyphs To Update")]
    [SerializeField] private List<ActionGlyphImage> actionGlyphImages = new List<ActionGlyphImage>();

    private Player player;
    private Controller lastController;
    private GlyphSet currentGlyphSet;

    private void Awake()
    {
        player = ReInput.players.GetPlayer(playerId);
        RefreshGlyphSet(true);
    }

    private void Update()
    {
        RefreshGlyphSet(false);
    }

    private void RefreshGlyphSet(bool forceUpdate)
    {
        Controller controller = player.controllers.GetLastActiveController();

        if (controller == null)
        {
            controller = ReInput.controllers.Keyboard;
        }

        if (!forceUpdate && controller == lastController)
        {
            return;
        }

        lastController = controller;
        currentGlyphSet = GetGlyphSet(controller);

        UpdateAllGlyphImages();
    }

    private GlyphSet GetGlyphSet(Controller controller)
    {
        if (controller == null)
        {
            return keyboardGlyphSet;
        }

        if (controller.type == ControllerType.Keyboard)
        {
            return keyboardGlyphSet;
        }

        if (controller.type == ControllerType.Joystick)
        {
            string controllerName = controller.name.ToLower();

            if (controllerName.Contains("xbox") || controllerName.Contains("xinput"))
            {
                return xboxGlyphSet;
            }

            if (controllerName.Contains("playstation") ||
                controllerName.Contains("dualshock") ||
                controllerName.Contains("dualsense") ||
                controllerName.Contains("wireless controller"))
            {
                return playStationGlyphSet;
            }

            return genericGamepadGlyphSet;
        }

        return keyboardGlyphSet;
    }

    private void UpdateAllGlyphImages()
    {
        if (currentGlyphSet == null)
        {
            return;
        }

        foreach (ActionGlyphImage actionGlyphImage in actionGlyphImages)
        {
            if (actionGlyphImage == null || actionGlyphImage.image == null)
            {
                continue;
            }

            Sprite glyph = currentGlyphSet.GetGlyph(actionGlyphImage.actionName);

            if (glyph != null)
            {
                actionGlyphImage.image.sprite = glyph;
                actionGlyphImage.image.enabled = true;
            }
            else
            {
                actionGlyphImage.image.enabled = false;
            }
        }
    }
}
