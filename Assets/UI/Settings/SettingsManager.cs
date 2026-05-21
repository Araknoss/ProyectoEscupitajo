using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; private set; }

    [SerializeField] private SettingsData _data;
    public SettingsData Data => _data;

    [Header("Audio Game Events")]
    [SerializeField] private GameEvent _onMasterVolumeChanged;
    [SerializeField] private GameEvent _onMusicVolumeChanged;
    [SerializeField] private GameEvent _onSFXVolumeChanged;
    [SerializeField] private GameEvent _onMuteChanged;

    [Header("Graphics Events")]
    [SerializeField] private GameEvent _onFullscreenChanged;
    [SerializeField] private GameEvent _onVSyncChanged;
    [SerializeField] private GameEvent _onTargetFPSChanged;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

        Load();
    }

    #region AUDIO

    public void SetMasterVolume(float value)
    {
        _data.masterVolume = value;

        _onMasterVolumeChanged?.Raise(this, value);

        Save();
    }

    public void SetMusicVolume(float value)
    {
        _data.musicVolume = value;

        _onMusicVolumeChanged?.Raise(this, value);

        Save();
    }

    public void SetSFXVolume(float value)
    {
        _data.sfxVolume = value;

        _onSFXVolumeChanged?.Raise(this, value);

        Save();
    }

    public void SetMute(bool value)
    {
        _data.muteAll = value;

        _onMuteChanged?.Raise(this, value);

        Save();
    }

    #endregion

    #region GRAPHICS

    public void SetFullscreen(bool value)
    {
        _data.fullscreen = value;

        _onFullscreenChanged?.Raise(this, value);

        Save();
    }

    public void SetVSync(bool value)
    {
        _data.vsync = value;

        _onVSyncChanged?.Raise(this, value);

        Save();
    }

    public void SetTargetFPS(float value)
    {
        int fps = Mathf.RoundToInt(value);

        _data.targetFPS = fps;

        _onTargetFPSChanged?.Raise(this, fps);

        Save();
    }

    #endregion

    public void ApplyAll()
    {
        ApplyAudio();
        ApplyGraphics();
    }

    private void ApplyAudio()
    {
        _onMasterVolumeChanged?.Raise(this, _data.masterVolume);

        _onMusicVolumeChanged?.Raise(this, _data.musicVolume);

        _onSFXVolumeChanged?.Raise(this, _data.sfxVolume);

        _onMuteChanged?.Raise(this, _data.muteAll);
    }

    private void ApplyGraphics()
    {
        _onFullscreenChanged?.Raise(this, _data.fullscreen);
        _onVSyncChanged?.Raise(this, _data.vsync);
        _onTargetFPSChanged?.Raise(this, _data.targetFPS);
    }
    public void Save()
    {
        SettingsRepository.Save(_data);
    }

    public void Load()
    {
        SettingsRepository.Load(_data);

        ApplyAll();
    }

    private void OnApplicationPause(bool paused)
    {
        if (paused)
            Save();
    }

    private void OnApplicationQuit()
    {
        Save();
    }
}
