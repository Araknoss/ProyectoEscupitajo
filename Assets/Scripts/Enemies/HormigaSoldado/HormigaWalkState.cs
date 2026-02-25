using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HormigaWalkState : State
{
    [SerializeField] private AnimationClip walkAnimation;   

    [SerializeField] private float movementSpeed;
    [SerializeField] private Vector3 walkDirection;

    private Vector3 startPosition;

    [Header("Raycast")]
    [SerializeField] private Transform leftRayCastOrigin;
    [SerializeField] private Transform rightRayCastOrigin;
    [SerializeField] private LayerMask groundLayer;
    private float raycastDistance = 1f;
    private bool canCheck = true;   
    public override void Enter()
    {
        if(walkAnimation != null)
            core.animator.Play(walkAnimation.name);
        core.gameObject.transform.localPosition = core.GetComponent<EnemyController>().startPosition;
        
    }
    public override void Do()
    {
        core.gameObject.transform.position += movementSpeed * Time.deltaTime * walkDirection;     
    }

    public override void FixedDo()
    {
        body.velocity = Vector2.zero;

        if (canCheck)
            CheckGround();       
    }
    public override void Exit() { }    

    private void CheckGround()
    {
        RaycastHit2D rightHit = Physics2D.Raycast(rightRayCastOrigin.position, Vector2.left, raycastDistance, groundLayer);
        Debug.DrawRay(rightRayCastOrigin.position, Vector2.left * raycastDistance, Color.blue, 1f);
       
        if (!rightHit.collider)
        {
            SwitchDirection();
        }
    }
    
    private void SwitchDirection()
    {
        canCheck = false;
        walkDirection = -walkDirection;
        StartCoroutine(WaitToCheckGround());
        Vector3 localScale = core.gameObject.transform.localScale;
        localScale.x *= -1; // Flip the x scale to change direction
        core.gameObject.transform.localScale = localScale;
        Debug.Log("Direction switched");
    }

    IEnumerator WaitToCheckGround()
    {
        yield return new WaitForSeconds(0.5f); // Wait for 0.5 seconds before checking ground again
        canCheck = true;
    }

}
