using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ParabolicMovementStaste : State
{
    [SerializeField] private AnimationClip animationClip;
    
    public Transform parent1;
    public Vector2 originalPosition;

    [SerializeField] Vector3 direction = Vector3.left;
    [SerializeField] private float movementSpeed;

    [Header("Parámetros parabólicos")]
    [SerializeField] private float duration = 2f;
    [SerializeField] private float height = 1f;
    // Posiciones de uso interno
    private Vector3 startWorldPosition;
    private Vector3 horizontalDisplacement;

    [SerializeField] private State nextState;

    public override void Enter()
    {
        core.actualState = this;
        if (animationClip != null)
        {
            animator.Play(animationClip.name);
        }            
        parent1 = core.gameObject.transform.parent;
        originalPosition = core.gameObject.transform.localPosition;
        core.gameObject.transform.SetParent(null);

        // Guardar inicio y calcular desplazamiento horizontal total
        startWorldPosition = core.gameObject.transform.position;
        Vector3 dir = direction.sqrMagnitude > 0.0001f ? direction.normalized : Vector3.right;
        horizontalDisplacement = dir * movementSpeed * duration;
    }
    public override void Do()
    {
        float parabolaY = 4f * height * time * (1f - time);
        Vector3 newPos = startWorldPosition + horizontalDisplacement * time + Vector3.up * parabolaY;
        core.gameObject.transform.position = newPos;

        //core.gameObject.transform.position += movementSpeed * Time.deltaTime * direction;
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
