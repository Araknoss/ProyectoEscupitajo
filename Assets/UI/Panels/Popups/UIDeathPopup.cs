using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIDeathPopup : UIPopup
{

    public override bool CanGoBack => false;
    [Header("Buttons")]
    [SerializeField] private Button retryButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button shopButton;

    [Header("Buttons Container")]
    [SerializeField] private CanvasGroup buttonsGroup;

    [Header("Texts")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private TMP_Text demoText;

    [Header("Feedbacks")]
    [SerializeField] private MMF_Player popupIntroFeedback;
    [SerializeField] private MMF_Player buttonsAppearFeedback;
    [SerializeField] private MMF_Player scoreToCoinsFeedback;

    [Header("Game Events")]
    [SerializeField] private GameEvent quitEvent;
    [SerializeField] private GameEvent retryEvent;
    [SerializeField] private GameEvent mainMenuEvent;
    [SerializeField] private GameEvent shopEvent;


    protected override void Awake()
    {
        retryButton.onClick.AddListener(OnRetryPressed);
        menuButton.onClick.AddListener(OnMenuPressed);
        shopButton.onClick.AddListener(OnShopPressed);
        quitButton.onClick.AddListener(OnQuitPressed);

    }

    protected override void OnShow()
    {
        //SelectDefaultButton();
        StartCoroutine(ShowSequence());
    }

    private void OnRetryPressed()
    {
        retryEvent?.Raise(this, null);
    }
    private void OnQuitPressed()
    {
        quitEvent?.Raise(this, null);
    }

    private void OnShopPressed()
    {
        shopEvent?.Raise(this, null);
    }
    private void OnMenuPressed()
    {
        mainMenuEvent?.Raise(this, null);
    }

    private void SelectDefaultButton()
    {
        if (defaultSelected == null)
            return;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(defaultSelected);
    }

    protected override void OnHide()
    {
        //resumeEvent.Raise(this, null);
    }

    //FEEDBACK SEQUENCE
    private IEnumerator ShowSequence()
    {
        // Ocultar botones
        buttonsGroup.alpha = 0;
        buttonsGroup.interactable = false;
        buttonsGroup.blocksRaycasts = false;

        // Mostrar score inicial       
        coinsText.text = "0";

        yield return new WaitForSecondsRealtime(1f);
        // Animación de entrada
        if (popupIntroFeedback != null)
        {
            popupIntroFeedback.PlayFeedbacks();
        }

        // Espera a que termine la animación
        yield return new WaitForSecondsRealtime(1f);

        // Conversión score -> monedas
        yield return StartCoroutine(AnimateScoreToCoins());

        // Mostrar botones
        if (buttonsAppearFeedback != null)
        {
            buttonsAppearFeedback.PlayFeedbacks();
        }

        buttonsGroup.alpha = 1;
        buttonsGroup.interactable = true;
        buttonsGroup.blocksRaycasts = true;

        SelectDefaultButton();
    }

    private IEnumerator AnimateScoreToCoins()
    {       
        if(GoldManager.Instance == null) yield break;
        int targetCoins = GoldManager.Instance.goldFromScore; 

        float duration = 1.0f;
        float elapsed = 0f;

        AudioManager.Instance.PlayScoreToCoinSound();

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;

            float t = Mathf.Clamp01(elapsed / duration);

            int displayedCoins = Mathf.RoundToInt(
                Mathf.Lerp(0, targetCoins, t));

            coinsText.text = displayedCoins.ToString();

            yield return null;
        }

        coinsText.text = targetCoins.ToString();
    }

    public void HandleOnDemoEnd(Component sender, object data)
    {
        if (popupIntroFeedback != null)
        {
            demoText.text = "DEMO END";
        }
    }

}
