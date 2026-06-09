using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Screens")]
    [SerializeField] private UIScreen initialScreen;
    [SerializeField] private List<UIScreen> screens;    
    private Dictionary<Type, UIScreen> screenMap;
    public UIScreen CurrentScreen => navigation.CurrentScreen;
    private UINavigationService navigation;

    [Header("Popups")]
    [SerializeField] private List<UIPopup> popups;    
    private Dictionary<Type, UIPopup> popupMap;
    public UIPopup CurrentPopup => popupService.CurrentPopup;
    private UIPopupService popupService;

    [SerializeField] private GameEvent onGameResumed;
    [SerializeField] private GameEvent onGamePaused;

    private void Awake()
    {
        navigation = new UINavigationService();        

        screenMap = new Dictionary<Type, UIScreen>();
        foreach (UIScreen screen in screens)
        {
            screenMap.Add(screen.GetType(), screen);
            screen.Hide();
        }

        popupService = new UIPopupService();

        popupMap = new Dictionary<Type, UIPopup>();

        foreach (UIPopup popup in popups)
        {
            popupMap.Add(popup.GetType(), popup);

            popup.Hide();
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
    public void OpenPopup<T>() where T : UIPopup
    {
        Type type = typeof(T);

        if (popupMap.TryGetValue(type, out UIPopup popup))
        {
            CurrentScreen?.CacheSelection();

            popupService.Show(popup);

            Debug.Log("OPEN POPUP: " + popup.name);
        }
    }
    public void Open(UIScreen screen)
    {
        if (screen == null)
            return;

        navigation.Push(screen);
    }

    public void ClosePopup()
    {
        popupService.Close();

        // --------------------------------
        // RESTORE FOCUS
        // --------------------------------

        CurrentScreen?.RestoreFocus();
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
            ClosePopup();
            //if(CurrentScreen is UIPauseScreen) //Para recuperar el foco
            //{
            //    Open<UIPauseScreen>();
            //}
            return;
        }

        // ----------------------------
        // SCREEN NAVIGATION
        // ----------------------------

        if(CurrentScreen is UIPauseScreen)
        {
            HandleOnResumePressed(this, null);
        }
        navigation.Pop();
        Debug.Log("POP" );
    }

    // ---------- EVENTS ----------

    public void HandleOnSettingsPressed(Component sender, object data)
    {
        Open<UISettingsScreen>();
    }

    public void HandleOnShopPressed(Component sender, object data)
    {
        Open<UIShopScreen>();
    }

    //public void OpenMainMenu(Component sender, object data)
    //{
    //    Replace<UIMainMenuScreen>();
    //}       

    public void HandleOnTrickUnlocked(Component sender, object data)
    {
        Open<UIUnlockScreen>();
    }

    public void HandleOnPausePressed(Component sender, object data)
    {
        onGamePaused.Raise(this, null); //Envia señal al sistema de tiempo y input para pausar el juego
        if(CurrentPopup != null)
        {
            popupService.CloseAll();
        }
        Open<UIPauseScreen>();
    }

    public void HandleOnResumePressed(Component sender, object data) //Envia señal al sistema de tiempo y input para reanudar el juego
    {
        if (!(CurrentScreen is UIGameplayScreen))
        {
            navigation.Pop();
        }

        onGameResumed.Raise(this, null);
    }

    public void HandleOnQuitPressed(Component sender, object data)
    {
        OpenPopup<UIQuitPopup>();
    }

    public void HandleOnDeath(Component sender, object data)
    {
        OpenPopup<UIDeathPopup>();
    }
    public void Quit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
