using UnityEngine;

public abstract class UIPanel : MonoBehaviour
{
    public bool IsVisible { get; private set; }

    public virtual bool CanGoBack => true;

    public virtual void Initialize() { }

    public virtual void Show()
    {
        IsVisible = true;
        gameObject.SetActive(true);

        OnShow();
    }

    public virtual void Hide()
    {
        IsVisible = false;

        OnHide();

        gameObject.SetActive(false);
    }

    protected virtual void OnShow() { }

    protected virtual void OnHide() { }

    public virtual void Refresh() { }
}
