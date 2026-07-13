using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }

    [Header("Estado actual (solo lectura, para depuración)")]
    [SerializeField] private bool isTutorialActive;
    [SerializeField] private bool isLoading;
    [SerializeField] private bool isPaused;

    public bool IsTutorialActive => isTutorialActive;
    public bool IsLoading => isLoading;
    public bool IsPaused => isPaused;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Determina si el juego puede pausarse en el estado actual.
    /// </summary>
    public bool CanPause()
    {
        return !isTutorialActive && !isLoading;
    }

    // -------- TUTORIAL --------

    public void HandleOnTutorialStart(Component sender, object data)
    {
        isTutorialActive = true;
    }

    public void HandleOnTutorialEnd(Component sender, object data)
    {
        isTutorialActive = false;
    }

    // -------- LOADING --------

    public void HandleOnLoadingStart(Component sender, object data)
    {
        isLoading = true;
    }

    public void HandleOnLoadingEnd(Component sender, object data)
    {
        isLoading = false;
    }

    // -------- PAUSE --------

    public void HandleOnGamePause(Component sender, object data)
    {
        isPaused = true;
    }

    public void HandleOnGameResume(Component sender, object data)
    {
        isPaused = false;
    }
}