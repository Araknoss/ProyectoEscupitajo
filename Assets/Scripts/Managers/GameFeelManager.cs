using System.Collections;
using UnityEngine;

public class GameFeelManager : MonoBehaviour
{
    public static GameFeelManager Instance;

    [Header("Freeze")]
    [SerializeField] private bool useFreeze = true;

    [Header("Shake")]
    [SerializeField] private bool useShake = true;
    [SerializeField] private Transform cameraTarget;
    [SerializeField] private bool shakeAffectsZ = false;

    private Coroutine freezeCoroutine;
    private Coroutine shakeCoroutine;

    private Vector3 originalLocalPosition;
    private float originalTimeScale = 1f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (cameraTarget == null)
        {
            cameraTarget = transform;
        }

        originalLocalPosition = cameraTarget.localPosition;
    }

    private void LateUpdate()
    {
        // Por si la cámara se recoloca en runtime y quieres mantener la referencia base actualizada
        if (shakeCoroutine == null)
        {
            originalLocalPosition = cameraTarget.localPosition;
        }
    }

    public void PlayImpact(float freezeDuration, float shakeDuration, float shakeStrength)
    {
        if (useFreeze)
        {
            Freeze(freezeDuration);
        }

        if (useShake)
        {
            Shake(shakeDuration, shakeStrength);
        }
    }

    public void Freeze(float duration)
    {
        if (freezeCoroutine != null)
        {
            StopCoroutine(freezeCoroutine);
        }

        freezeCoroutine = StartCoroutine(FreezeRoutine(duration));
    }

    private IEnumerator FreezeRoutine(float duration)
    {
        originalTimeScale = Time.timeScale;
        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = 1f;
        freezeCoroutine = null;
    }

    public void Shake(float duration, float strength)
    {
        if (shakeCoroutine != null)
        {
            StopCoroutine(shakeCoroutine);
            cameraTarget.localPosition = originalLocalPosition;
        }

        shakeCoroutine = StartCoroutine(ShakeRoutine(duration, strength));
    }

    private IEnumerator ShakeRoutine(float duration, float strength)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;

            float offsetX = Random.Range(-1f, 1f) * strength;
            float offsetY = Random.Range(-1f, 1f) * strength;
            float offsetZ = shakeAffectsZ ? Random.Range(-1f, 1f) * strength : 0f;

            cameraTarget.localPosition = originalLocalPosition + new Vector3(offsetX, offsetY, offsetZ);

            yield return null;
        }

        cameraTarget.localPosition = originalLocalPosition;
        shakeCoroutine = null;
    }

    public void LightHit()
    {
        PlayImpact(0.03f, 0.08f, 0.08f);
    }

    public void MediumHit()
    {
        PlayImpact(0.05f, 0.12f, 0.14f);
    }

    public void HeavyHit()
    {
        PlayImpact(0.08f, 0.18f, 0.22f);
    }
}
