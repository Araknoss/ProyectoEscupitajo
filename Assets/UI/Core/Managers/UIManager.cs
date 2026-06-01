using System;
using System.Collections.Generic;
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

    [SerializeField] private GameEvent onGameResumed;
    [SerializeField] private GameEvent onGamePaused;

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

        if(CurrentScreen is UIPauseScreen)
        {
            ResumeGame(this, null);
        }
        navigation.Pop();
        Debug.Log("POP" );
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

    //public void OpenMainMenu(Component sender, object data)
    //{
    //    Replace<UIMainMenuScreen>();
    //}       

    public void OpenUnlockScreen(Component sender, object data)
    {
        Open<UIUnlockScreen>();
    }

    public void OpenPauseMenu(Component sender, object data)
    {
        onGamePaused.Raise(this, null); //Envia señal al sistema de tiempo y input para pausar el juego
        Open<UIPauseScreen>();
    }

    public void ResumeGame(Component sender, object data) //Envia señal al sistema de tiempo y input para reanudar el juego
    {
        if (!(CurrentScreen is UIGameplayScreen))
        {
            navigation.Pop();
        }

        onGameResumed.Raise(this, null);
    }

    public void Quit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
