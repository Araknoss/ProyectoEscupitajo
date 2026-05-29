using System.Collections.Generic;

public class UIPopupService
{
    private readonly Stack<UIPopup> popupStack = new();

    public UIPopup CurrentPopup =>
        popupStack.Count > 0 ? popupStack.Peek() : null;

    public bool HasPopup =>
        popupStack.Count > 0;

    // ----------------------------
    // SHOW
    // ----------------------------

    public void Show(UIPopup popup)
    {
        popupStack.Push(popup);

        popup.Show();
    }

    // ----------------------------
    // CLOSE TOP
    // ----------------------------

    public void Close()
    {
        if (popupStack.Count == 0)
            return;

        UIPopup popup = popupStack.Pop();

        popup.Hide();
    }

    // ----------------------------
    // CLOSE SPECIFIC
    // ----------------------------

    public void Close(UIPopup popup)
    {
        if (!popupStack.Contains(popup))
            return;

        Stack<UIPopup> temp = new();

        while (popupStack.Count > 0)
        {
            UIPopup current = popupStack.Pop();

            if (current == popup)
            {
                current.Hide();
                break;
            }

            temp.Push(current);
        }

        while (temp.Count > 0)
        {
            popupStack.Push(temp.Pop());
        }
    }

    // ----------------------------
    // CLOSE ALL
    // ----------------------------

    public void CloseAll()
    {
        while (popupStack.Count > 0)
        {
            UIPopup popup = popupStack.Pop();

            popup.Hide();
        }
    }
}
