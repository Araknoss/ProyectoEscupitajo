using UnityEngine;
using UnityEngine.UI;
using Rewired;

public class RewiredButtonInvoker : MonoBehaviour
{
    [SerializeField] private int playerId = 0;
    [SerializeField] private string actionName = "Confirm";
    [SerializeField] private Button button;

    private Player player;

    private void Awake()
    {
        player = ReInput.players.GetPlayer(playerId);
    }

    private void Update()
    {
        if (button == null || player == null)
            return;

        if (!button.interactable)
            return;

        if (player.GetButtonDown(actionName))
        {
            button.onClick.Invoke();
        }
    }
}
