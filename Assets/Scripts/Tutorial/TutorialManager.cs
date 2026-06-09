using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    private List<TutorialStep> steps;

    public int currentStepIndex;

    public TutorialStep CurrentStep;

    [SerializeField] UnityEvent onFirstTutorialStepCompleted;
    [SerializeField] UnityEvent onSecondTutorialStepCompleted;
    [SerializeField] UnityEvent onThirdTutorialStepCompleted;
    [SerializeField] UnityEvent onFourthTutorialStepCompleted;
    [SerializeField] UnityEvent onTutorialCompleted;

    private void Start()
    {
        currentStepIndex = 0;
        CurrentStep = steps[currentStepIndex];
        StartStep(0);
    }

    private void Update()
    {
        if (CurrentStep.IsCompleted())
        {
            NextStep();
        }
    }

    private void StartStep(int index)
    {
        currentStepIndex = index;

        CurrentStep.EnterStep();
    }

    private void NextStep()
    {
        CurrentStep.ExitStep();

        StepEvent();

        currentStepIndex++;        

        if (currentStepIndex >= steps.Count)
        {
            FinishTutorial();
            return;
        }

        CurrentStep = steps[currentStepIndex];

        StartStep(currentStepIndex);
    }

    private void FinishTutorial()
    {
        onTutorialCompleted.Invoke();
        Debug.Log("Tutorial Complete");
    }

    private void StepEvent()
    {
        if (currentStepIndex == 0)
        {
            onFirstTutorialStepCompleted.Invoke();
        }
        if (currentStepIndex == 1)
        {
            onSecondTutorialStepCompleted.Invoke();
        }
        if (currentStepIndex == 2)
        {
            onThirdTutorialStepCompleted.Invoke();
        }
        if (currentStepIndex == 3)
        {
            onFourthTutorialStepCompleted.Invoke();
        }


    }

}
