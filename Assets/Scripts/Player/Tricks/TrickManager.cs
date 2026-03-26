using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class TrickManager : MonoBehaviour
{
    [SerializeField] private List<Trick> tricksPerformed = new List<Trick>();
    [SerializeField] private List<Trick> availableTricks = new List<Trick>();
    [SerializeField] private List<Trick> baseTricks = new List<Trick>();
    [SerializeField] private List<Trick> wallBaseTricks = new List<Trick>();
    private Trick lastTrickPerformed;   

    [Header("Input")]
    [SerializeField] private KeyCode bodyKey= KeyCode.J;
    private bool bodyInput;
    [SerializeField] private KeyCode skateKey= KeyCode.K;
    private bool skateInput;
    [SerializeField] private KeyCode keepKey= KeyCode.Space;    
    private bool keepInputPress;
    private bool keepInputRelease;

    [Header("Variables")]
    [SerializeField] private float trickCooldownTime=0.2f; //Este tiempo depende de cada truco
    [SerializeField] private float trickCooldownTimer;
    private bool onCombo;
    [SerializeField] private float listenInputOffset;

    [Header("KeepTricksVariables")]
    private bool onKeepTrick;
    [SerializeField] private float performKeepTrickTime = 0.1f;
    private float performKeepTrickTimer;

    [Header("KeepTrickTiming")]
    [SerializeField] private float keepTimingSpeed = 1f;
    [SerializeField] private float chargeTimingSpeedModifier = 2f;
    private float originalKeepTimingSpeed;
    [SerializeField] private float minRange = 0.4f;
    [SerializeField] private float maxRange = 0.6f;
    public float keepTiming { private set; get; }
    private bool isInsideRange;

    [Header("Timing")]
    [SerializeField] private float trickPerfectTimingPercentage = 0.2f;
    [SerializeField] private float trickPerfectTime;   
    private bool isPerfectTiming;
    [SerializeField] private float trickGreatTimingPercentage = 0.5f;
    [SerializeField] private float trickGreatTime;
    private bool isGreatTiming;


    [Header("OnWall")]
    [SerializeField] private Trick wallSlideTrick;
    [SerializeField] private Trick wallJumpTrick;
    [SerializeField] private Trick wallChargeTrick;
    [SerializeField] private float wallScoreTime = 0.1f;
    public bool isOnWall=false;
    private bool isOnWallSlide=false;
    private bool isOnWallCharge=false;
    private float wallScoreTimer;        

    [Header("Events")]
    public GameEvent onTrickPerformed;
    public GameEvent onTrickPerformedOnPerfectTiming;
    public GameEvent onKeepingTrick;
    //public GameEvent onWallSlidePerformed;
    public GameEvent onWallSlideEnd;
    public GameEvent onAvailableTricksReset;
    public GameEvent onComboEnd;
    public GameEvent onPerfectTiming;
    public GameEvent onGreatTiming;

    [Header("Debug")]
    [SerializeField] private Animator animator;

    private void Start()
    {
        SetAvailableTricks(baseTricks);
        originalKeepTimingSpeed = keepTimingSpeed;
    }
    private void Update()
    {
        InitializeInput();
        HandleInput();
        //if (isOnWallSlide)
        //{
        //    HandleWallSlide();
        //    HandleKeepTiming();
        //    return;
        //}
        if (onKeepTrick || isOnWallSlide)
        {
            HandleKeepTrick();
            HandleKeepTiming();
            return;
        }        
        HandleTrickCooldown();       
        //HandleBodyTricks();
    }
    void InitializeInput()
    {
        bodyInput = Input.GetKeyDown(bodyKey);
        skateInput= Input.GetKeyDown(skateKey);
        keepInputPress = Input.GetKeyDown(keepKey);
        keepInputRelease = Input.GetKeyUp(keepKey);
    }    
    void HandleInput() //Solo se pueden hacer trucos cuando estan disponibles en la lista AvailableTricks
    {
        if(bodyInput && !onKeepTrick)
        {
            CheckAvailable(bodyKey);
        }
        if(skateInput && !onKeepTrick)
        {
            CheckAvailable(skateKey);
        }
        if(keepInputPress && !onKeepTrick) 
        {         
            CheckAvailable(keepKey);
        }
        if(keepInputRelease && onKeepTrick)
        {
            onKeepTrick = false;
            PerformTrick(availableTricks[0]); //Se asume que solo hay un truco al soltar           
        }
    }
    void CheckAvailable(KeyCode input) //Se comprueba si el input corresponde a algun truco disponible
    {
        if(availableTricks.Any())
        {
            for(int i=0;i<availableTricks.Count;i++)
            {
                if(availableTricks[i].inputKey == input)
                {
                    if (!availableTricks[i].isStateTrick) //Para que los trucos de estado solo se activen desde los estados
                    {
                        PerformTrick(availableTricks[i]);
                        return;
                    }
                }
            }
        }        
    }    
    void HandleTrickCooldown()
    {
        if (trickCooldownTimer > 0f && onCombo)
        {
            //if (!isOnWall)
            //{
            trickCooldownTimer -= Time.deltaTime;
            //}
            if (trickCooldownTimer < trickGreatTime && !isGreatTiming)
            {
                SetGreatTiming(true);
            }
            if (trickCooldownTimer < trickPerfectTime && !isPerfectTiming)
            {                
                SetPerfectTiming(true);               
            }
        }
        else if(onCombo) //Cuando el cooldown termina se resetean los trucos disponibles
        {            
            ResetCombo();
        }
    }

    void SetAvailableTricks(List<Trick> newAvailableTricks)
    {
        availableTricks.Clear();        

        for (int i=0;i< newAvailableTricks.Count;i++)
        {
            if (UnlockablesManager.Instance.HasUnlockedTrick(newAvailableTricks[i]) || newAvailableTricks[i].isUnlockedAtStart) //Si el truco es base siempre lo puedes usar
            {
                availableTricks.Add(newAvailableTricks[i]);
            }            
        }
        onAvailableTricksReset.Raise(this, availableTricks);

        SetPerfectTiming(false);
        SetGreatTiming(false);
    }

    void PerformTrick(Trick trick)
    {
        if(isPerfectTiming)
        {
            onTrickPerformedOnPerfectTiming.Raise(this, isPerfectTiming);
        }
        else if (isGreatTiming)
        {
            onTrickPerformed.Raise(this, isGreatTiming);
        }       
        else if (onCombo && !trick.isStateTrick) 
        {
            ResetCombo();
            return;
        }
        onTrickPerformed.Raise(this, trick);

        tricksPerformed.Add(trick);      
        onCombo = true;

        //Times
        ResetTimes(trick);
                      
        SetAvailableTricks(trick.comboTricks);

        lastTrickPerformed = trick;
        if (trick.isKeepTrick)
        {
            onKeepTrick = true;
        }
    }  
    
    void PerformWallSlideTrick() //Se ejecuta al entrar en contacto con la pared 1 vez
    {            
        onTrickPerformed.Raise(this, wallSlideTrick);
        SetAvailableTricks(wallBaseTricks);
    }

    void WallSlideEnd() //Se ejecuta al salir de la pared o al hacer un wall jump o wall charge
    {
        onWallSlideEnd.Raise(this, wallSlideTrick);
        ResetTimes(wallSlideTrick);
        isOnWallSlide = false;
    }

    private void ResetTimes(Trick trick)
    {
        trickCooldownTime = trick.listenInputTime; //El tiempo en el que el jugador puede pulsar el input para hacer el siguiente truco
        trickCooldownTimer = trickCooldownTime + listenInputOffset; //El tiempo total que tiene el jugador para hacer el siguiente truco, incluyendo offset general a lo coyote time
        trickPerfectTime = trickCooldownTime * trickPerfectTimingPercentage; //El momento a partir del cual el jugador tiene un timing perfecto para hacer el siguiente truco
        trickGreatTime = trickCooldownTime * trickGreatTimingPercentage;
    }    

    private void ResetCombo()
    {
        SetAvailableTricks(baseTricks);

        onCombo = false;
        onKeepTrick=false;        

        trickCooldownTimer = trickCooldownTime;
        onComboEnd.Raise(this, true);
        //Debug
        if (!isOnWall)
        {
            animator.SetTrigger("Idle");
        } 
    }    
    void HandleKeepTrick()
    {        
            performKeepTrickTimer += Time.deltaTime;
            if (performKeepTrickTimer > performKeepTrickTime)
            {
                performKeepTrickTimer = 0;
                onKeepingTrick.Raise(this, lastTrickPerformed);                 
            }    
    }
    void HandleKeepTiming()
    {
        keepTiming = Mathf.PingPong(Time.time * keepTimingSpeed, 1f);
        bool currentlyInside = keepTiming >= minRange && keepTiming <= maxRange;        
        if (currentlyInside && !isInsideRange)
        {
            SetPerfectTiming(true);
        }
        
        if (!currentlyInside && isInsideRange)
        {
            SetGreatTiming(true);
        }

        isInsideRange = currentlyInside;
    }
    private void SetPerfectTiming(bool isPerfect)
    {
        isPerfectTiming = isPerfect;
        onPerfectTiming.Raise(this, isPerfectTiming);       
    }

    private void SetGreatTiming(bool isGreat)
    {
        isGreatTiming = isGreat;
        onGreatTiming.Raise(this, isGreatTiming);
    }

    public void SetIsOnWall(Component sender, object data)
    {
        if (data is bool)
        {
            isOnWall = (bool)data;
            isOnWallSlide=(bool)data;
            onKeepTrick = false;

            if (!isOnWall /*&& lastTrickPerformed == wallSlideTrick*/) //Asi solo se activa cuando sales de la pared sin usar trucos
            {                           
                SetAvailableTricks(baseTricks);
                //WallSlideEnd();


            }
            else //Cuando entras en contacto se triggerea el truco
            {
                PerformWallSlideTrick();                            }

        }
    }
    public void HandleOnWallJump(Component sender, object data)
    {
        if(data is bool)
        {
            bool hasWallJumped = (bool)data;
            if (hasWallJumped)
            {
                PerformTrick(wallJumpTrick);
            }              
        }
    }

    public void HandleOnWallCharge(Component sender, object data)
    {
        if(data is bool)
        {         
            bool isCharging = (bool)data;
            if (isCharging)
            {
                PerformTrick(wallChargeTrick);
                keepTimingSpeed *= chargeTimingSpeedModifier;
            }
            else
            {
                keepTimingSpeed = originalKeepTimingSpeed;
            }
                
        }
    }

    public void HandleOnWallChargeFailed(Component sender, object data)
    {
        ResetCombo();
    }

}
