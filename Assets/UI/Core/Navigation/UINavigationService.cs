using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINavigationService : MonoBehaviour
{
    private readonly Stack<UIScreen> screenStack = new();

    public UIScreen CurrentScreen =>
        screenStack.Count > 0 ? screenStack.Peek() : null;

    public void Push(UIScreen screen)
    {
        if (CurrentScreen != null)
            CurrentScreen.Hide();

        screenStack.Push(screen);

        screen.Show();

        Debug.Log($"Pushed screen: {screen.name}. Stack count: {screenStack.Count}");
    }

    public void Pop()
    {
        if (screenStack.Count == 0)
            return;

        UIScreen current = screenStack.Peek();

        if (!current.CanGoBack)
            return;

        current.Hide();

        screenStack.Pop();

        if (screenStack.Count > 0)
        {
            screenStack.Peek().Show();
        }
    }

    public void Clear()
    {
        while (screenStack.Count > 0)
        {
            screenStack.Pop().Hide();
        }
    }
}
