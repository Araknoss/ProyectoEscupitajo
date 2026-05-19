// UIPanel.cs

using Rewired;
using UnityEngine;

public abstract class UIPanel : MonoBehaviour
{
    public bool IsVisible { get; private set; }    

    protected virtual void Awake()
    {
       
    }

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

    protected virtual void OnShow()
    {
    }

    protected virtual void OnHide()
    {
    }

    public virtual void HandleInput(Player player)
    {
    }

    public virtual bool CanGoBack => true;
}
