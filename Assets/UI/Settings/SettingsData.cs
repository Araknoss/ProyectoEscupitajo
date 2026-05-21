// SettingsData.cs
using UnityEngine;

[CreateAssetMenu(fileName = "SettingsData", menuName = "Runner/Settings Data")]
public class SettingsData : ScriptableObject
{
    [Header("Audio")]
    [Range(0f, 1f)] public float masterVolume = 1f;
    [Range(0f, 1f)] public float musicVolume = 0.8f;
    [Range(0f, 1f)] public float sfxVolume = 1f;
    public bool muteAll = false;

    [Header("Gráficos")]
    public bool fullscreen = true;
    public bool vsync = true;
    public int targetFPS = 60;
    public int resolutionIndex = 0;   // índice en Screen.resolutions

    [Header("Gameplay")]
    public float cameraShakeIntensity = 1f;
    public bool screenFlash = true;

    [Header("Accesibilidad")]
    public bool colorblindMode = false;
    public int colorblindType = 0;    // 0=Deuteranopía, 1=Protanopía, 2=Tritanopía
    public float uiScale = 1f;

    public void ResetToDefaults()
    {
        masterVolume = 1f;
        musicVolume = 0.8f;
        sfxVolume = 1f;
        muteAll = false;
        fullscreen = true;
        vsync = true;
        targetFPS = 60;
        resolutionIndex = 0;
        cameraShakeIntensity = 1f;
        screenFlash = true;
        colorblindMode = false;
        colorblindType = 0;
        uiScale = 1f;
    }
}
