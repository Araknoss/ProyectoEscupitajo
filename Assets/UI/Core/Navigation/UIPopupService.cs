using System;
using System.Collections.Generic;

public class UIPopupService
{
    private readonly Stack<UIPopup> popupStack = new();

    public UIPopup CurrentPopup =>
        popupStack.Count > 0 ? popupStack.Peek() : null;

    public bool HasPopup =>
        popupStack.Count > 0;

    // ------------------------------------------------
    // SHOW
    // ------------------------------------------------

    public void Show(UIPopup popup)
    {
        if (popup == null)
            return;

        popupStack.Push(popup);

        popup.Show();
    }

    // ------------------------------------------------
    // CLOSE TOP
    // ------------------------------------------------

    public void Close()
    {
        if (popupStack.Count == 0)
            return;

        UIPopup popup = popupStack.Pop();

        popup.Hide();
    }

    // ------------------------------------------------
    // CLOSE ALL
    // ------------------------------------------------

    public void CloseAll()
    {
        while (popupStack.Count > 0)
        {
            UIPopup popup = popupStack.Pop();

            popup.Hide();
        }
    }
}
