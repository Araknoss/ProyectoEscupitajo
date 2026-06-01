
using System.Collections.Generic;
using System.Diagnostics;

public class UINavigationService
{
    private readonly Stack<UIScreen> screenStack = new();

    public UIScreen CurrentScreen =>
        screenStack.Count > 0 ? screenStack.Peek() : null;

    public void Push(UIScreen screen)
    {
        if (CurrentScreen != null)
        {
            CurrentScreen.Hide();
        }

        screenStack.Push(screen);

        screen.Show();
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

    public void Replace(UIScreen screen)
    {
        if (screenStack.Count > 0)
        {
            UIScreen current = screenStack.Pop();

            current.Hide();
        }

        screenStack.Push(screen);

        screen.Show();
    }
}
