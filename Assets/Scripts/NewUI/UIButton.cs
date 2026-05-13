using UnityEngine;
using UnityEngine.Events;

public class UIButton : UISelectable
{
    [SerializeField] private UnityEvent _onSubmit;

    // Visual feedback al ganar/perder foco
    public override void OnFocusGained()
    {
        base.OnFocusGained();
        transform.localScale = Vector3.one * 1.05f; // pequeño scale-up
    }

    public override void OnFocusLost()
    {
        transform.localScale = Vector3.one;
    }

    public override void OnSubmit()
    {
        _onSubmit?.Invoke();
    }
}
