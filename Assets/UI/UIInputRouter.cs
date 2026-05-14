using Rewired;
using UnityEngine;

public class UIInputRouter : MonoBehaviour
{  
    private Player player;

    [Header("GameEvents")]
    [SerializeField] private GameEvent backEvent;

    private void Awake()
    {
        player = ReInput.players.GetPlayer(0);

        Debug.Log("UIInputRouter initialized. Player: " + player.name);
    }

    private void Update()
    {
        if (player.GetButtonDown("UICancel"))
        {
            Debug.Log("Back button pressed");
            backEvent.Raise(this, null);
        }
        
        
        if (player.GetButtonDown("UISubmit"))
        {
            //UIManager.HandleSubmit();
        }
    }
}
