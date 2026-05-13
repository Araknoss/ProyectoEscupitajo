// SettingsManager.cs
using System;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; private set; }

    [SerializeField] private SettingsData _data;
    public SettingsData Data => _data;

    public event Action OnSettingsChanged;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Load();
    }

    public void Apply()
    {
        OnSettingsChanged?.Invoke();
    }

    public void Save()
    {
        SettingsRepository.Save(_data);
    }

    public void Load()
    {
        SettingsRepository.Load(_data);
        Apply();
    }

    void OnApplicationPause(bool paused) { if (paused) Save(); }
    void OnApplicationQuit() { Save(); }
}
