using UnityEngine;
using MoreMountains.Feedbacks;
using FMODUnity;

[AddComponentMenu("")]
[FeedbackHelp("Reproduce un evento de FMOD")]
[FeedbackPath("Custom/FMOD Sound")]
public class MMF_FMODSound : MMF_Feedback
{
    [Header("FMOD")]
    public EventReference fmodEvent;

    protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1.0f)
    {
        if (!Active || fmodEvent.IsNull) return;

        RuntimeManager.PlayOneShot(fmodEvent, position);
    }
}
