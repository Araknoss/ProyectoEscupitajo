using System.Collections.Generic;
using Rewired;
using UnityEngine;

[System.Serializable]
public class RequiredInput
{
    public string actionName;

    public bool positive;
}
public class AxisInputTutorialStep : TutorialStep
{
    [SerializeField]
    private List<RequiredInput> requiredInputs;

    private HashSet<int> completedInputs =
        new HashSet<int>();

    private Player player;    

    private void Awake()
    {
        player = ReInput.players.GetPlayer(0);
    }

    public override void EnterStep()
    {
        completedInputs.Clear();
    }    

    public override bool IsCompleted()
    {
        for (int i = 0; i < requiredInputs.Count; i++)
        {
            if (completedInputs.Contains(i))
                continue;

            RequiredInput input =
                requiredInputs[i];

            float axis =
                player.GetAxisRaw(input.actionName);

            bool pressed =
                input.positive
                ? axis > 0.5f
                : axis < -0.5f;

            if (pressed)
            {
                completedInputs.Add(i);

                Debug.Log(
                    $"Completed {input.actionName}");               
            }
        }

        return completedInputs.Count ==
               requiredInputs.Count;
    }

    public bool IsInputCompleted(int index)
    {
        return completedInputs.Contains(index);
    }
}
