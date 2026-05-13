using UnityEngine;

public class MainMenuPanel : UIPanel
{
    [Header("Buttons")]
    [SerializeField] private UIButton _btnPlay;
    [SerializeField] private UIButton _btnSettings;
    [SerializeField] private UIButton _btnQuit;

    private void Awake()
    {
        // Navegación vertical entre botones
        _btnPlay.NavDown = _btnSettings;
        _btnSettings.NavUp = _btnPlay;
        _btnSettings.NavDown = _btnQuit;
        _btnQuit.NavUp = _btnSettings;

        DefaultSelectable = _btnPlay;
    }
}
