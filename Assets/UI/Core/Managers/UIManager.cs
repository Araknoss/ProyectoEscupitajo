using Rewired;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Screens")]
    [SerializeField] private UIMainMenuScreen mainMenu;
    [SerializeField] private UISettingsScreen settings;
    //[SerializeField] private UIShopScreen shop;

    private UINavigationService navigation;    

    private void Awake()
    {
        navigation = new UINavigationService();        
    }
    
    private void Start()
    {
        navigation.Push(mainMenu);
    }

    public void HandleOpenSettings(Component sender, object data)
    {
        navigation.Push(settings);
    }

    public void OpenShop()
    {
        //navigation.Push(shop);
    }

    public void HandleBack(Component sender, object data)
    {
        navigation.Pop();
    }
   

    public static void HandleSubmit()
    {
        //
    }
}
