using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : State
{
    [SerializeField] private AnimationClip idleAnimation;
    [SerializeField] private float movementSpeed;
    [SerializeField] private Vector3 direction;
    public override void Enter()
    {
        animator.Play(idleAnimation.name);
    }
    public override void Do()
    {
        core.gameObject.transform.position += movementSpeed * Time.deltaTime * direction;
        Debug.Log("RataLataMoving");
    }

    public override void FixedDo()
    {
        body.velocity = Vector2.zero;
    }
    public override void Exit() { }

    public void SetDirection(Vector3 dir)
    {
        if (dir == Vector3.zero) return;
        direction = dir.normalized;
    }

    public void SetHorizontalDirection(int directionSign)
    {
        if (directionSign == 0) return;
        direction = new Vector3(Mathf.Sign(directionSign), 0f, 0f);
    }

    public void SetSpeed(float speed)
    {
        movementSpeed = speed;
    }
}
