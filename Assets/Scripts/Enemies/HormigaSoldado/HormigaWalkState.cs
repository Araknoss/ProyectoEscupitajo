using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HormigaWalkState : State
{
    [SerializeField] private AnimationClip walkAnimation;
    [SerializeField] private float speed;

    [SerializeField] private float movementSpeed;
    [SerializeField] private Vector3 walkDirection;

    private Vector3 startPosition;
    public override void Enter()
    {
        if(walkAnimation != null)
            core.animator.Play(walkAnimation.name);
        core.gameObject.transform.localPosition = core.GetComponent<EnemyController>().startPosition;
        Debug.Log(startPosition);
    }
    public override void Do()
    {
        core.gameObject.transform.position += movementSpeed * Time.deltaTime * walkDirection;
    }

    public override void FixedDo()
    {
        body.velocity = Vector2.zero;
    }
    public override void Exit() { }    

}
