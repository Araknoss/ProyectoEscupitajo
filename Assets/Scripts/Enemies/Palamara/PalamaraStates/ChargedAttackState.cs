using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedAttackState : State
{
    [SerializeField] private AnimationClip animationClip;
    [SerializeField] private AnimationClip detectedClip;

    [SerializeField] private float attackDelay;
    [SerializeField] private float attackDuration;
    [SerializeField] private float movementSpeed;
    [SerializeField] private GameObject attackTrigger;
    private bool onAttack;

    public Transform parent1;
    public Vector2 originalPosition;

    public override void Enter()
    {
        animator.Play(detectedClip.name);
        attackTrigger.SetActive(true);
        onAttack = false;
        parent1 = core.gameObject.transform.parent;
        originalPosition = core.gameObject.transform.localPosition;
    }
    public override void Do()
    {
        if(time>=detectedClip.length+attackDelay && !onAttack)
        {
            core.gameObject.transform.SetParent(null);
            float playerPosY = FindAnyObjectByType<PlayerController>().gameObject.transform.position.y;
            core.gameObject.transform.position = new Vector3(15, playerPosY, core.gameObject.transform.position.z);
            onAttack = true;            
        }
        else if(onAttack)
        {
            Attack();
        }
    }
    public override void Exit() 
    {
        animator.Play(animationClip.name);
        attackTrigger.SetActive(false);
        onAttack = false;
        core.gameObject.transform.SetParent(parent1);
        Debug.Log("PamaraExitAttack");
        core.gameObject.transform.localPosition = originalPosition;
    }   
    
    private void Attack()
    {
        core.gameObject.transform.position += movementSpeed * Time.deltaTime * Vector3.left;
        if (time >= detectedClip.length + attackDelay + attackDuration)
        {
            isComplete = true;
        }
    }
}
