using Rewired;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIShopScreen : UIScreen
{
    [System.Serializable]
    public class ShopTabView
    {
        public ShopTreeDataSO data;
        [Space]
        public GameObject content;
        [Space]
        public GameObject defaultSelected;
    }

    [Header("Tabs")]
    [SerializeField] private ShopTabView[] tabs;

    [Header("UI")]
    [SerializeField] private TMP_Text tabNameText;

    [Header("Input")]
    [SerializeField] private string nextTabAction = "UIR1";
    [SerializeField] private string previousTabAction = "UIL1";

    private int currentTab;

    protected override void OnShow()
    {
        base.OnShow();
        RefreshTabs();
    }

    public override void HandleInput(Player player)
    {
        if (player.GetButtonDown(nextTabAction))
            NextTab();

        if (player.GetButtonDown(previousTabAction))
            PreviousTab();
    }

    private void NextTab()
    {
        currentTab++;
        if (tabs == null || tabs.Length == 0) return;
        if (currentTab >= tabs.Length) currentTab = 0;
        RefreshTabs();
    }

    private void PreviousTab()
    {
        currentTab--;
        if (tabs == null || tabs.Length == 0) return;
        if (currentTab < 0) currentTab = tabs.Length - 1;
        RefreshTabs();
    }

    private void RefreshTabs()
    {
        if (tabs == null || tabs.Length == 0) return;

        for (int i = 0; i < tabs.Length; i++)
        {
            bool isCurrent = i == currentTab;
            if (tabs[i].content != null)
                tabs[i].content.SetActive(isCurrent);
        }

        RefreshCurrentTabUI();
        SelectDefault();
    }

    private void RefreshCurrentTabUI()
    {
        if (tabNameText != null && tabs != null && tabs.Length > 0 && tabs[currentTab] != null && tabs[currentTab].data != null)
            tabNameText.text = tabs[currentTab].data.treeName;
    }

    private void SelectDefault()
    {
        if (tabs == null || tabs.Length == 0) return;

        GameObject target = tabs[currentTab].defaultSelected;
        if (target == null) return;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(target);
    }
}
