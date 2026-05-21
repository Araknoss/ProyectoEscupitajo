using UnityEngine;
using UnityEngine.UI;

public class UISettingsController : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;
    [SerializeField] private Toggle _muteToggle;

    [Header("Graphics")]
    [SerializeField] private Toggle _fullscreenToggle;
    [SerializeField] private Toggle _vsyncToggle;
    [SerializeField] private Slider _fpsSlider;

    [Header("Gameplay")]
    [SerializeField] private Slider _shakeSlider;
    [SerializeField] private Toggle _flashToggle;

    private SettingsManager Settings => SettingsManager.Instance;
    private SettingsData Data => Settings.Data;

    private void OnEnable()
    {
        RefreshAllControls();
    }

    private void OnDisable()
    {
        UnregisterListeners();
    }

    private void RefreshAllControls()
    {
        UnregisterListeners();

        // AUDIO
        if(_masterSlider != null)
            _masterSlider.value = Data.masterVolume;
        if(_musicSlider != null)
        _musicSlider.value = Data.musicVolume;
        if(_sfxSlider != null)
            _sfxSlider.value = Data.sfxVolume;
        if(_muteToggle != null)
            _muteToggle.isOn = Data.muteAll;

        // GRAPHICS
        //_fullscreenToggle.isOn = Data.fullscreen;
        //_vsyncToggle.isOn = Data.vsync;
        //_fpsSlider.value = Data.targetFPS;

        // GAMEPLAY
        //_shakeSlider.value = Data.cameraShakeIntensity;
        //_flashToggle.isOn = Data.screenFlash;

        RegisterListeners();
    }

    private void RegisterListeners()
    {
        // AUDIO
        _masterSlider?.onValueChanged.AddListener(Settings.SetMasterVolume);

        _musicSlider?.onValueChanged.AddListener(Settings.SetMusicVolume);

        _sfxSlider?.onValueChanged.AddListener(Settings.SetSFXVolume);

        _muteToggle?.onValueChanged.AddListener(Settings.SetMute);

        // GRAPHICS
        _fullscreenToggle?.onValueChanged.AddListener(Settings.SetFullscreen);

        _vsyncToggle?.onValueChanged.AddListener(Settings.SetVSync);

        _fpsSlider?.onValueChanged.AddListener(Settings.SetTargetFPS);

        // GAMEPLAY
        //_shakeSlider?.onValueChanged.AddListener(Settings.SetCameraShakeIntensity);
        //_flashToggle?.onValueChanged.AddListener(Settings.SetScreenFlash);
    }

    private void UnregisterListeners()
    {
        _masterSlider?.onValueChanged.RemoveAllListeners();
        _musicSlider?.onValueChanged.RemoveAllListeners();
        _sfxSlider?.onValueChanged.RemoveAllListeners();
        _muteToggle?.onValueChanged.RemoveAllListeners();

        _fullscreenToggle?.onValueChanged.RemoveAllListeners();
        _vsyncToggle?.onValueChanged.RemoveAllListeners();
        _fpsSlider?.onValueChanged.RemoveAllListeners();

        //_shakeSlider?.onValueChanged.RemoveAllListeners();
        //_flashToggle?.onValueChanged.RemoveAllListeners();
    }    

    public void OnResetClicked()
    {
        Data.ResetToDefaults();

        Settings.ApplyAll();

        RefreshAllControls();
    }
}
