using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TricksManager : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [Header("State")]
    [SerializeField] private bool isInWall;     

    [Header("Components")]
    [SerializeField] private ComboManager comboManager;

    [Header("Events")]
    [SerializeField] private GameEvent onTrickPerformed;

    [Header("Wall Tricks")]
    [SerializeField] private Trick wallSlide;
    [SerializeField] private Trick wallJump;
    private float inWallTimer;
    private float inWallTrickThreshold = 0.1f;



    private float xInput;
    private float yInput;
    private bool trickInput;
    private void Awake()
    {
        if (comboManager == null)
        {
            comboManager = GetComponent<ComboManager>();
        }
    }
    private void Update()
    {
        CheckInputs();        
        HandleWallTricks();
    }
    private void CheckInputs()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        trickInput = Input.GetButtonDown("Jump");
    }
    private void HandleWallTricks()
    {
        if (isInWall)
        {
            inWallTimer += Time.deltaTime;
            if(inWallTimer >= inWallTrickThreshold)
            {
                inWallTimer = 0f;
                onTrickPerformed.Raise(this, wallSlide);                
            }
            if(trickInput)
            {
                onTrickPerformed.Raise(this, wallJump);
                animator.SetTrigger("wallJump");
            }
        }
        else
        {
            inWallTimer = 0f;
        }
    }
    public void SetIsInWall(Component sender, object data)
    {
        if(data is bool)
        {
            isInWall = (bool)data;
            animator.SetBool("isInWall", isInWall);
        }       
    }
}

