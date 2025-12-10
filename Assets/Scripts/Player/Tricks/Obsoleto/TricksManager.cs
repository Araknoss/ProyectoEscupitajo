using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TricksManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float trickCooldown;
    private float trickCooldownTimer=0f;
    private bool isOnCooldown = false;

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

    [Header("Flip Tricks")]
    [SerializeField] private Trick horizontalFlip;
    [SerializeField] private Trick backFlip;
    [SerializeField] private float backFlipBufferTime = 0.3f;
    private bool canDoBackFlip = false;
    private float backFlipBufferTimer = 0f;
  
    [Header("Inputs")]
    private bool jumpInput;
    private bool grabTrickInput;
    private bool flipTrickInput;
    [SerializeField] private KeyCode grabTrickKey;
    [SerializeField] private KeyCode flipTrickKey;
    //[SerializeField] private float inputBufferTime = 0.1f;
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
        HandleOnAirTricks();
        HandleBackFlipBuffer();
        HandleTrickCooldown();
    }
    private void CheckInputs()
    {
        //xInput = Input.GetAxisRaw("Horizontal");
        //yInput = Input.GetAxisRaw("Vertical");
        jumpInput = Input.GetButtonDown("Jump");
        grabTrickInput = Input.GetKeyDown(grabTrickKey);
        flipTrickInput = Input.GetKeyDown(flipTrickKey);
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
            if(jumpInput)
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
    

    private void HandleOnAirTricks()
    {
        if (grabTrickInput && !isInWall && !isOnCooldown)
        {
            onTrickPerformed.Raise(this, horizontalFlip);
            canDoBackFlip = true;
            backFlipBufferTimer = backFlipBufferTime;
            //animator.SetTrigger("horizontalFlip");
            animator.Play(horizontalFlip.animationClip.name);
            isOnCooldown = true; //Solo se lo añado aqui para que no puedas spamear el combo pero si puedas encadenarlo
        }
        else if (flipTrickInput && canDoBackFlip && !isInWall)
        {
            onTrickPerformed.Raise(this, backFlip);
            canDoBackFlip = false;
            backFlipBufferTimer = 0f;
            //animator.SetTrigger("backFlip");
            animator.Play(backFlip.animationClip.name);
        }
    }

    private void HandleBackFlipBuffer()
    {
        if (canDoBackFlip)
        {
            backFlipBufferTimer -= Time.deltaTime;
            if (backFlipBufferTimer <= 0f)
            {
                canDoBackFlip = false;
                backFlipBufferTimer = backFlipBufferTime;
            }
        }
    }

    private void HandleTrickCooldown()
    {
        if (isOnCooldown)
        {
            trickCooldownTimer += Time.deltaTime;
            if (trickCooldownTimer >= trickCooldown)
            {
                isOnCooldown = false;
                trickCooldownTimer = 0;
            }
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

