using Rewired;
using UnityEngine;

public class GlyphButtonFeedback : MonoBehaviour
{
    [SerializeField] private int playerId = 0;
    [SerializeField] private string actionName = "Jump";

    private Player player;

    private Vector3 normalScale;
    private bool wasPressed;

    private void Awake()
    {
        player = ReInput.players.GetPlayer(playerId);
        normalScale = transform.localScale;
    }

    private void Update()
    {
        bool pressed = player.GetButton(actionName);

        if (pressed && !wasPressed)
        {
            transform.localScale = normalScale * 0.85f;
        }
        else if (!pressed && wasPressed)
        {
            transform.localScale = normalScale;
        }

        wasPressed = pressed;
    }
}