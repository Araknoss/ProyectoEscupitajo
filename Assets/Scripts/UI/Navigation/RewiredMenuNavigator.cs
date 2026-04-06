using System;
using Rewired;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RewiredMenuNavigator : MonoBehaviour
{
    public enum InputMode
    {
        KeyboardMouse,
        Gamepad
    }

    [Header("Rewired")]
    [SerializeField] private int playerId = 0;

    [Header("Menu")]
    [SerializeField] private Selectable firstSelected;
    [SerializeField] private bool autoSelectFirstOnEnable = true;
    [SerializeField] private bool autoReselectWhenUsingGamepad = true;
    [SerializeField] private bool clearSelectionWhenUsingMouse = false;

    [Header("Mouse Detection")]
    [SerializeField] private bool detectMouseMovement = true;
    [SerializeField] private float mouseMoveThreshold = 0.1f;
    [SerializeField] private bool detectMouseButtons = true;

    public InputMode CurrentMode { get; private set; } = InputMode.KeyboardMouse;
    public event Action<InputMode> OnInputModeChanged;

    private Player player;
    private EventSystem currentEventSystem;
    private Vector3 lastMousePosition;

    private void Awake()
    {
        currentEventSystem = EventSystem.current;

        if (!ReInput.isReady)
        {
            Debug.LogError("[RewiredMenuNavigator] ReInput is not ready.");
            enabled = false;
            return;
        }

        player = ReInput.players.GetPlayer(playerId);

        if (player == null)
        {
            Debug.LogError($"[RewiredMenuNavigator] Player with id {playerId} was not found.");
            enabled = false;
            return;
        }

        if (currentEventSystem == null)
        {
            Debug.LogError("[RewiredMenuNavigator] No EventSystem found in scene.");
            enabled = false;
            return;
        }
    }

    private void OnEnable()
    {
        if (!ReInput.isReady || player == null)
            return;

        player.controllers.AddLastActiveControllerChangedDelegate(OnLastActiveControllerChanged);

        lastMousePosition = Input.mousePosition;

        RefreshCurrentModeFromPlayer();

        if (autoSelectFirstOnEnable && CurrentMode == InputMode.Gamepad)
        {
            EnsureSelection();
        }
    }

    private void OnDisable()
    {
        if (!ReInput.isReady || player == null)
            return;

        player.controllers.RemoveLastActiveControllerChangedDelegate(OnLastActiveControllerChanged);
    }

    private void Update()
    {
        if (currentEventSystem == null)
            return;

        DetectKeyboardMouseUsage();

        if (CurrentMode == InputMode.Gamepad && autoReselectWhenUsingGamepad)
        {
            EnsureSelection();
        }
    }

    private void OnLastActiveControllerChanged(Player changedPlayer, Controller controller)
    {
        if (changedPlayer == null || changedPlayer.id != playerId)
            return;

        RefreshCurrentModeFromPlayer();

        if (CurrentMode == InputMode.Gamepad)
        {
            EnsureSelection();
        }
    }

    private void RefreshCurrentModeFromPlayer()
    {
        Controller lastController = player.controllers.GetLastActiveController();

        if (lastController == null)
        {
            SetInputMode(InputMode.KeyboardMouse);
            return;
        }

        switch (lastController.type)
        {
            case ControllerType.Joystick:
                SetInputMode(InputMode.Gamepad);
                break;

            case ControllerType.Keyboard:
            case ControllerType.Mouse:
            default:
                SetInputMode(InputMode.KeyboardMouse);
                break;
        }
    }

    private void DetectKeyboardMouseUsage()
    {
        bool mouseMoved = false;
        bool mouseClicked = false;
        bool keyboardUsed = false;

        if (detectMouseMovement)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            mouseMoved = (currentMousePosition - lastMousePosition).sqrMagnitude > mouseMoveThreshold * mouseMoveThreshold;
            lastMousePosition = currentMousePosition;
        }

        if (detectMouseButtons)
        {
            mouseClicked = Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2);
        }

        // Teclado básico para volver a modo KB/M aunque no cambie todavía el last active controller del Player.
        keyboardUsed =
            Input.anyKeyDown &&
            !Input.GetMouseButtonDown(0) &&
            !Input.GetMouseButtonDown(1) &&
            !Input.GetMouseButtonDown(2);

        if (mouseMoved || mouseClicked || keyboardUsed)
        {
            SetInputMode(InputMode.KeyboardMouse);

            if (clearSelectionWhenUsingMouse)
            {
                ClearSelection();
            }
        }
    }

    private void SetInputMode(InputMode newMode)
    {
        if (CurrentMode == newMode)
            return;

        CurrentMode = newMode;
        OnInputModeChanged?.Invoke(CurrentMode);
    }

    private void EnsureSelection()
    {
        if (currentEventSystem.currentSelectedGameObject != null)
            return;

        Selectable target = GetBestSelectable();

        if (target == null)
            return;

        currentEventSystem.SetSelectedGameObject(target.gameObject);
    }

    private Selectable GetBestSelectable()
    {
        if (IsSelectableValid(firstSelected))
            return firstSelected;

        Selectable[] selectables = FindObjectsOfType<Selectable>(true);

        for (int i = 0; i < selectables.Length; i++)
        {
            if (IsSelectableValid(selectables[i]))
                return selectables[i];
        }

        return null;
    }

    private bool IsSelectableValid(Selectable selectable)
    {
        if (selectable == null)
            return false;

        if (!selectable.gameObject.activeInHierarchy)
            return false;

        if (!selectable.IsInteractable())
            return false;

        return true;
    }

    public void ForceSelectFirst()
    {
        if (currentEventSystem == null)
            return;

        Selectable target = GetBestSelectable();

        if (target == null)
            return;

        currentEventSystem.SetSelectedGameObject(target.gameObject);
    }

    public void ClearSelection()
    {
        if (currentEventSystem == null)
            return;

        currentEventSystem.SetSelectedGameObject(null);
    }

    public void SetFirstSelected(Selectable selectable)
    {
        firstSelected = selectable;
    }

    public bool IsUsingGamepad()
    {
        return CurrentMode == InputMode.Gamepad;
    }

    public bool IsUsingKeyboardMouse()
    {
        return CurrentMode == InputMode.KeyboardMouse;
    }
}