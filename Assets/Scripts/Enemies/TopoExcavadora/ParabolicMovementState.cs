using UnityEngine;

public class ParabolicMovementStaste : State
{
    [SerializeField] private AnimationClip animationClip;

    public Transform parent1;
    public Vector2 originalPosition;

    [SerializeField] private float duration = 3f; // Duración total del movimiento

    [Header("Movement")]
    [SerializeField] private Vector3 direction = Vector3.left;
    [SerializeField] private float movementSpeed = 2f;
    [SerializeField] private float distance = 3f;

    [Header("Parabola")]
    [SerializeField] private float height = 2f; // altura máxima de la parábola

    [SerializeField] private State nextState;

    private Vector3 startPosition;
    private Vector3 targetPosition;

    public override void Enter()
    {
        core.actualState = this;

        if (animationClip != null)
            animator.Play(animationClip.name);

        parent1 = core.gameObject.transform.parent;
        originalPosition = core.gameObject.transform.localPosition;

        // Desacoplamos para mover en mundo sin depender del padre
        core.gameObject.transform.SetParent(null);

        // Preparamos trayectoria
        startPosition = core.gameObject.transform.position;

        Vector3 dir = direction.sqrMagnitude > 0f ? direction.normalized : Vector3.left;
        targetPosition = startPosition + dir * distance;      

        // Duración basada en velocidad y distancia (si duration no está fijada en el State)
        // Si ya tienes duration configurada desde fuera y NO quieres tocarla, borra esta línea.
        duration = Mathf.Max(0.01f, distance / Mathf.Max(0.01f, movementSpeed));
    }

    public override void Do()
    {      
        float t = Mathf.Clamp01(time / duration);

        // Movimiento lineal base
        Vector3 linearPos = Vector3.Lerp(startPosition, targetPosition, t);

        // Offset parabólico (máximo en t=0.5)
        float parabola = 4f * height * t * (1f - t);

        // Aplico la parábola en Y (vertical)
        core.gameObject.transform.position = new Vector3(
            linearPos.x,
            linearPos.y + parabola,
            linearPos.z
        );

        if (time >= duration)
        {
            isComplete = true;
            Set(nextState, true);
        }
    }

    public override void Exit()
    {
        core.gameObject.transform.SetParent(parent1);
        core.gameObject.transform.localPosition = originalPosition;
    }
}
