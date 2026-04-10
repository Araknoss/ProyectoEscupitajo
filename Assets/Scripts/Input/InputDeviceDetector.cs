using UnityEngine;
using Rewired;

public class InputDeviceDetector : MonoBehaviour
{
    private bool subscribed;

    private void OnEnable()
    {
        ReInput.InitializedEvent += OnRewiredInitialized;
        ReInput.ShutDownEvent += OnRewiredShutdown;

        if (ReInput.isReady)
        {
            Subscribe();
        }
    }

    private void OnDisable()
    {
        Unsubscribe();

        ReInput.InitializedEvent -= OnRewiredInitialized;
        ReInput.ShutDownEvent -= OnRewiredShutdown;
    }

    private void OnRewiredInitialized()
    {
        Subscribe();
    }

    private void OnRewiredShutdown()
    {
        subscribed = false;
    }

    private void Subscribe()
    {
        if (subscribed || !ReInput.isReady) return;

        ReInput.controllers.AddLastActiveControllerChangedDelegate(OnLastActiveControllerChanged);
        subscribed = true;
    }

    private void Unsubscribe()
    {
        if (!subscribed || !ReInput.isReady) return;

        ReInput.controllers.RemoveLastActiveControllerChangedDelegate(OnLastActiveControllerChanged);
        subscribed = false;
    }

    private void OnLastActiveControllerChanged(Controller controller)
    {
        if (controller == null) return;

        if (controller.type == ControllerType.Joystick)
        {
            Debug.Log("Controller");
            Cursor.visible = false;
        }
        else
        {
            Debug.Log("Keyboard/Mouse");
            Cursor.visible = true;
        }
    }
}
