// UIManager.cs

using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Screens")]
    [SerializeField] private List<UIScreen> screens;

    private Dictionary<Type, UIScreen> screenMap;

    private UINavigationService navigation;

    public UIScreen CurrentScreen => navigation.CurrentScreen;

    private void Awake()
    {
        navigation = new UINavigationService();

        screenMap = new Dictionary<Type, UIScreen>();

        foreach (UIScreen screen in screens)
        {
            screenMap.Add(screen.GetType(), screen);

            screen.Hide();
        }
    }

    private void Start()
    {
        Open<UIMainMenuScreen>();
    }

    public void Open<T>() where T : UIScreen
    {
        Type type = typeof(T);

        if (screenMap.TryGetValue(type, out UIScreen screen))
        {
            navigation.Push(screen);
        }
    }

    public void Replace<T>() where T : UIScreen
    {
        Type type = typeof(T);

        if (screenMap.TryGetValue(type, out UIScreen screen))
        {
            navigation.Replace(screen);
        }
    }

    public void Back(Component sender, object data)
    {
        navigation.Pop();
        Debug.Log("Back" +CurrentScreen);  
    }

    // ---------- EVENTS ----------

    public void OpenSettings(Component sender, object data)
    {
        Open<UISettingsScreen>();
    }

    public void OpenShop(Component sender, object data)
    {
        Open<UIShopScreen>();
    }

    public void OpenMainMenu(Component sender, object data)
    {
        Replace<UIMainMenuScreen>();
    }
}
