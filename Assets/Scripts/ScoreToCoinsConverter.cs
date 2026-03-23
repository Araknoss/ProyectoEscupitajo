using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ScoreToCoinsConverter : MonoBehaviour
{
    [Header("Conversion Settings")]
    [SerializeField] private int conversionFactor = 100; // 100 puntos = 1 moneda
    [SerializeField] private float conversionInterval = 0.05f; // tiempo entre cada conversión

    [Header("Current Values")]
    [SerializeField] private int pendingScore;
    [SerializeField] private int currentCoins;

    [Header("Optional Events")]
    public GameEvent onPendingScoreChanged;
    public GameEvent onCoinsChanged;
    public GameEvent onConversionFinished;

    private Coroutine conversionCoroutine;
    private bool isConverting;

    public int PendingScore => pendingScore;
    public int CurrentCoins => currentCoins;
    public bool IsConverting => isConverting;

    /// <summary>
    /// Añade puntuación pendiente de convertir.
    /// </summary>
    public void AddScore(int amount)
    {
        pendingScore += amount;
        onPendingScoreChanged.Raise(this, pendingScore);
    }

    /// <summary>
    /// Inicia la conversión de puntuación a monedas poco a poco.
    /// </summary>
    public void StartConversion()
    {
        if (isConverting || pendingScore < conversionFactor)
            return;

        conversionCoroutine = StartCoroutine(ConvertScoreOverTime());
    }

    private IEnumerator ConvertScoreOverTime()
    {
        isConverting = true;

        while (pendingScore >= conversionFactor)
        {
            pendingScore -= conversionFactor;
            currentCoins += 1;

            onPendingScoreChanged.Raise(this, pendingScore);
            onCoinsChanged.Raise(this, currentCoins);

            yield return new WaitForSeconds(conversionInterval);
        }

        isConverting = false;
        conversionCoroutine = null;
        onConversionFinished?.Raise(this, 0);
    }

    /// <summary>
    /// Convierte toda la puntuación restante de golpe.
    /// </summary>
    public void ConvertAllInstantly()
    {
        if (conversionCoroutine != null)
        {
            StopCoroutine(conversionCoroutine);
            conversionCoroutine = null;
        }

        int coinsToAdd = pendingScore / conversionFactor;
        int remainingScore = pendingScore % conversionFactor;

        currentCoins += coinsToAdd;
        pendingScore = remainingScore;

        onPendingScoreChanged.Raise(this, pendingScore);
        onCoinsChanged?.Raise(this, currentCoins);

        isConverting = false;
        onConversionFinished?.Raise(this, 0);
    }

    private void Update()
    {
        // Ejemplo: clic izquierdo para convertir todo de golpe
        if (isConverting && Input.GetMouseButtonDown(0))
        {
            ConvertAllInstantly();
        }
    }
}

