using Rewired;
using UnityEngine;

public class InputTutorialStep : TutorialStep
{
    public string actionName;

    private Player player;    

    private void Awake()
    {
        player = ReInput.players.GetPlayer(0);
    }

    public override bool IsCompleted()
    {            
        return player.GetButtonDown(actionName);
    }
}
