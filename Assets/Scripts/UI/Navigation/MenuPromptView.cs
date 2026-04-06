using UnityEngine;

public class MenuPromptView : MonoBehaviour
{
    [SerializeField] private RewiredMenuNavigator navigator;

    private void OnEnable()
    {
        navigator.OnInputModeChanged += HandleInputModeChanged;
    }

    private void OnDisable()
    {
        navigator.OnInputModeChanged -= HandleInputModeChanged;
    }

    private void HandleInputModeChanged(RewiredMenuNavigator.InputMode mode)
    {
        if (mode == RewiredMenuNavigator.InputMode.Gamepad)
        {
            Debug.Log("Mostrar prompts de mando");
        }
        else
        {
            Debug.Log("Mostrar prompts de teclado/ratón");
        }
    }
}
