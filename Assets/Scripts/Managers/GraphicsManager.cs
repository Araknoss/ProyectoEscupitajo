using UnityEngine;

public class GraphicsManager : MonoBehaviour
{
    public void HandleOnFullscreenChanged(Component sender, object data)
    {
        bool fullscreen = (bool)data;

        Screen.fullScreen = fullscreen;
    }

    public void HandleOnVSyncChanged(Component sender, object data)
    {
        bool vsync = (bool)data;

        QualitySettings.vSyncCount = vsync ? 1 : 0;
    }

    public void HandleOnTargetFPSChanged(Component sender, object data)
    {
        int fps = (int)data;

        Application.targetFrameRate = fps;
    }
}
