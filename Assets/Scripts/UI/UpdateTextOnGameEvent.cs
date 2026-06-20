using MoreMountains.Feedbacks;
using TMPro;
using UnityEngine;

public class UpdateTextOnGameEvent : MonoBehaviour, IFeedback
{
    private TextMeshProUGUI targetText;
    [SerializeField] private string prefix;

    [Header("Delta thresholds")]
    [Tooltip("Delta mínimo para considerar un cambio y reproducir feedback.")]
    [SerializeField] private int minDelta = 10;
    [Tooltip("Delta usado como máximo para normalizar (delta >= maxDelta => t = 1).")]
    [SerializeField] private int maxDelta = 50;

    [Header("Feedbacks")]
    [Tooltip("Feedback principal que recibirá la intensidad en función del delta (se usará si no se asignan Small/Medium/Large).")]
    [SerializeField] private MMF_Player mainFeedback;
    [SerializeField] private MMF_Player smallFeedback;
    [SerializeField] private MMF_Player mediumFeedback;
    [SerializeField] private MMF_Player largeFeedback;

    [Header("Main feedback intensity")]
    [Tooltip("Intensidad mínima enviada al mainFeedback cuando delta == minDelta.")]
    [SerializeField] private float minFeedbackIntensity = 0.5f;
    [Tooltip("Intensidad máxima enviada al mainFeedback cuando delta >= maxDelta.")]
    [SerializeField] private float maxFeedbackIntensity = 1.5f;

    private int lastValue = 0;

    void Awake()
    {
        targetText = GetComponent<TextMeshProUGUI>();        
    }

    public void PlayFeedback()
    {
        //mainFeedback?.PlayFeedbacks();
    }

    public void UpdateScoreText(Component sender, object data)
    {
        if (targetText == null) return;
        if (!(data is int newValue)) return;
    
        targetText.text = newValue.ToString() + prefix;

        int delta = Mathf.Abs(newValue - lastValue);

        if (delta < minDelta)
        {
            lastValue = newValue;
            return;
        }

        float t = (maxDelta == minDelta) ? 1f : Mathf.InverseLerp(minDelta, maxDelta, delta);
        t = Mathf.Clamp01(t);

        // Si hay feedbacks por rango asignados, elegir uno por tramo
        if (smallFeedback != null || mediumFeedback != null || largeFeedback != null)
        {
            if (t < 0.33f)
            {
                smallFeedback?.PlayFeedbacks();
            }
            else if (t < 0.66f)
            {
                mediumFeedback?.PlayFeedbacks();
            }
            else
            {
                largeFeedback?.PlayFeedbacks();
            }
        }
        //else
        //{
        //    // Usar mainFeedback con intensidad mapeada entre minFeedbackIntensity y maxFeedbackIntensity
        //    if (mainFeedback != null)
        //    {
        //        float intensity = Mathf.Lerp(minFeedbackIntensity, maxFeedbackIntensity, t);
        //        mainFeedback.PlayFeedbacks(transform.position, intensity);
        //    }
        //}

        lastValue = newValue;
    }
}
