using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;
    [SerializeField] private Toggle _muteToggle;

    [Header("Gráficos")]
    [SerializeField] private Toggle _fullscreenToggle;
    [SerializeField] private Toggle _vsyncToggle;
    [SerializeField] private Slider _fpsSlider;

    [Header("Gameplay")]
    [SerializeField] private Slider _shakeSlider;
    [SerializeField] private Toggle _flashToggle;

    private SettingsData Data => SettingsManager.Instance.Data;

    void OnEnable()
    {
        RefreshAllControls();
    }

    void RefreshAllControls()
    {
        // Desconectar listeners para que asignar .value no dispare OnValueChanged
        UnregisterListeners();

        _masterSlider.value = Data.masterVolume;
        _musicSlider.value = Data.musicVolume;
        _sfxSlider.value = Data.sfxVolume;
        _muteToggle.isOn = Data.muteAll;
        _fullscreenToggle.isOn = Data.fullscreen;
        _vsyncToggle.isOn = Data.vsync;
        _fpsSlider.value = Data.targetFPS;
        //_shakeSlider.value = Data.cameraShakeIntensity;
        //_flashToggle.isOn = Data.screenFlash;

        RegisterListeners();
    }

    void RegisterListeners()
    {
        _masterSlider?.onValueChanged.AddListener(v => { Data.masterVolume = v; Apply(); });
        _musicSlider?.onValueChanged.AddListener(v => { Data.musicVolume = v; Apply(); });
        _sfxSlider?.onValueChanged.AddListener(v => { Data.sfxVolume = v; Apply(); });
        _muteToggle?.onValueChanged.AddListener(v => { Data.muteAll = v; Apply(); });

        _fullscreenToggle?.onValueChanged.AddListener(v => { Data.fullscreen = v; Apply(); });
        _vsyncToggle?.onValueChanged.AddListener(v => { Data.vsync = v; Apply(); });
        _fpsSlider?.onValueChanged.AddListener(v => { Data.targetFPS = Mathf.RoundToInt(v); Apply(); });

        //_shakeSlider?.onValueChanged.AddListener(v => { Data.cameraShakeIntensity = v; Apply(); });
        //_flashToggle?.onValueChanged.AddListener(v => { Data.screenFlash = v; Apply(); });
    }

    void UnregisterListeners()
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

    void Apply()
    {
        SettingsManager.Instance.Apply();
    }

    public void OnResetClicked()
    {
        Data.ResetToDefaults();
        SettingsManager.Instance.Apply();
        SettingsManager.Instance.Save();
        RefreshAllControls();
    }
}
