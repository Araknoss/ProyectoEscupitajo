using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : UIPanel
{
    [Header("Buttons")]
    [SerializeField] private Button _btnPlay;
    [SerializeField] private Button _btnSettings;
    [SerializeField] private Button _btnQuit;

    [Header("Panels")]
    [SerializeField] private UIPanel _settingsPanel;
    [SerializeField] private UIPanel _quitPanel;

    [Header("Panel Content")]
    [SerializeField] private GameObject _panelContent; 
    protected override GameObject PanelContent => _panelContent;

    private void Awake()
    {
        _btnPlay.onClick.AddListener(OnPlayClicked);
        _btnSettings.onClick.AddListener(OnSettingsClicked);
        _btnQuit.onClick.AddListener(OnQuitClicked);
    }

    private void Start()
    {
        UIManager.Instance.PushPanel(this);
    }
    private void OnPlayClicked()
    {
        // Carga tu escena de juego
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    private void OnSettingsClicked()
    {
        UIManager.Instance.PushPanel(_settingsPanel);
    }

    private void OnQuitClicked()
    {
        Application.Quit();
    }

    public override void OnCancel()
    {
        UIManager.Instance.PushPanel(_quitPanel);
    }

}
