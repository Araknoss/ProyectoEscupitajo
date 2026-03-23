using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

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
    private bool isOnWallCharge=false;
    private float wallScoreTimer;        

    [Header("Events")]
    public GameEvent onTrickPerformed;
    public GameEvent onTrickPerformedOnPerfectTiming;
    public GameEvent onKeepTrickPerformed;
    public GameEvent onWallSlidePerformed;
    public GameEvent onAvailableTricksReset;
    public GameEvent onComboEnd;
    public GameEvent onPerfectTiming;
    public GameEvent onGreatTiming;

    [Header("Debug")]
    [SerializeField] private Animator animator;

    private void Start()
    {
        SetAvailableTricks(baseTricks);
    }
    private void Update()
    {
        InitializeInput();
        HandleInput();
        if (onKeepTrick)
        {
            HandleKeepTrick();
            return;
        }        
        HandleTrickCooldown();
        HandleWallSlide();
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
            //Si el truco es de mantener la tecla, se comprueba tanto al pulsar como al soltar para permitir trucos que se activen al soltar la tecla
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

    private void ResetTimes(Trick trick)
    {
        trickCooldownTime = trick.listenInputTime; //El tiempo en el que el jugador puede pulsar el input para hacer el siguiente truco
        trickCooldownTimer = trickCooldownTime + listenInputOffset; //El tiempo total que tiene el jugador para hacer el siguiente truco, incluyendo offset general a lo coyote time
        trickPerfectTime = trickCooldownTime * trickPerfectTimingPercentage; //El momento a partir del cual el jugador tiene un timing perfecto para hacer el siguiente truco
        trickGreatTime = trickCooldownTime * trickGreatTimingPercentage;
    }
    //IEnumerator PerformKeepTrick(Trick trick)
    //{
    //    onKeepTrick = true;
    //    while(onKeepTrick) //Sales cuando sueltas el input
    //    {
    //        yield return new WaitForSeconds(performKeepTrickTimer);
    //        onKeepTrickPerformed.Raise(this, trick);
    //    }

    //}

    private void ResetCombo()
    {
        SetAvailableTricks(baseTricks);
        onCombo = false;
        trickCooldownTimer = trickCooldownTime;

        onComboEnd.Raise(this, true);

        //Debug
        if (!isOnWall)
        {
            animator.SetTrigger("Idle");
        } 
    }
    void HandleWallSlide()
    {
        if (isOnWall)
        {
            wallScoreTimer += Time.deltaTime;
            if (wallScoreTimer > wallScoreTime)
            {
                wallScoreTimer = 0;
                onWallSlidePerformed.Raise(this, wallSlideTrick);
            }
        }
    }

    void HandleKeepTrick()
    {        
            performKeepTrickTimer += Time.deltaTime;
            if (performKeepTrickTimer > performKeepTrickTime)
            {
                performKeepTrickTimer = 0;
                onKeepTrickPerformed.Raise(this, lastTrickPerformed);
            }
    
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
            onKeepTrick = false;
            if (!isOnWall)
            {
                //wallScoreTimer = 0f;
                SetAvailableTricks(baseTricks);                
                onTrickPerformed.Raise(this, wallSlideTrick.listenInputTime);
                ResetTimes(wallSlideTrick);

            }
            else //Cuando entras en contacto se triggerea el truco
            {
                SetAvailableTricks(wallBaseTricks);
                //PerformTrick(wallSlideTrick);
            }

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
            }
                
        }
    }

}
