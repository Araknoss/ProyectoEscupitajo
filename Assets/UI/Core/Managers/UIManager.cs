using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Screens")]
    [SerializeField] private List<UIScreen> screens;

    [Header("Initial Screen")]
    [SerializeField] private UIScreen initialScreen;

    private Dictionary<Type, UIScreen> screenMap;
    private UIPopupService popupService;
    public UIPopup CurrentPopup => popupService.CurrentPopup;

    private UINavigationService navigation;
    public UIScreen CurrentScreen => navigation.CurrentScreen;

    private void Awake()
    {
        navigation = new UINavigationService();
        popupService = new UIPopupService();

        screenMap = new Dictionary<Type, UIScreen>();
        foreach (UIScreen screen in screens)
        {
            screenMap.Add(screen.GetType(), screen);
            screen.Hide();
        }              
    }

    private void Start()
    {
        if (initialScreen != null)
        {
            Open(initialScreen);
        }
    }

    public void Open<T>() where T : UIScreen
    {
        Type type = typeof(T);
        if (screenMap.TryGetValue(type, out UIScreen screen))
        {
            navigation.Push(screen);
        }
    }
    public void Open(UIScreen screen)
    {
        if (screen == null)
            return;

        navigation.Push(screen);
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
        // ----------------------------
        // POPUP PRIORITY
        // ----------------------------

        if (popupService.HasPopup)
        {
            popupService.Close();
            return;
        }

        // ----------------------------
        // SCREEN NAVIGATION
        // ----------------------------

        navigation.Pop();
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

    public void OpenUnlockScreen(Component sender, object data)
    {
        Open<UIUnlockScreen>();
    }

    public void OpenPauseMenu(Component sender, object data)
    {
        Open<UIPauseScreen>();
    }

    public void Quit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
