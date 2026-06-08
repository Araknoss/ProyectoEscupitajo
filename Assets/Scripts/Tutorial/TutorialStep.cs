using UnityEngine;

public abstract class TutorialStep : MonoBehaviour
{
    public string stepText;

    public virtual void EnterStep()
    {
        gameObject.SetActive(true);
    }

    public virtual void ExitStep()
    {
        gameObject.SetActive(false);
    }

    public abstract bool IsCompleted();
}
