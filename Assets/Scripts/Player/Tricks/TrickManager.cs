using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrickManager : MonoBehaviour
{
    [SerializeField] private List<Trick> tricksPerformed = new List<Trick>();
    [SerializeField] private List<Trick> availableTricks = new List<Trick>();
    [SerializeField] private List<Trick> baseTricks = new List<Trick>();    
    //public Trick lastTrickPerformed;

    [Header("Input")]
    [SerializeField] private KeyCode bodyKey= KeyCode.J;
    private bool bodyInput;
    [SerializeField] private KeyCode skateKey= KeyCode.K;
    private bool skateInput;
    [SerializeField] private KeyCode keepKey= KeyCode.Space;
    private bool keepInput;

    [Header("Variables")]
    [SerializeField] private float trickCooldownTime=0.2f; //Este tiempo depende de cada truco
    [SerializeField] private float trickCooldownTimer;
    private bool onCombo;
    [SerializeField] private float listenInputOffset;

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
        HandleTrickCooldown();
        HandleWallSlide();
        //HandleBodyTricks();
    }
    void InitializeInput()
    {
        bodyInput = Input.GetKeyDown(bodyKey);
        skateInput= Input.GetKeyDown(skateKey);
        keepInput = Input.GetKeyDown(keepKey);
    }    
    void HandleInput() //Solo se pueden hacer trucos cuando estan disponibles en la lista AvailableTricks
    {
        if(bodyInput)
        {
            CheckAvailable(bodyKey);
        }
        if(skateInput)
        {
            CheckAvailable(skateKey);
        }
        if(keepInput)
        {         
            CheckAvailable(keepKey);
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
                    if (!availableTricks[i].isStateTrick) //Para que los trucos se activen solo desde el estado
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
            if (!isOnWall)
            {
                trickCooldownTimer -= Time.deltaTime;
            }
            if(trickCooldownTimer < trickGreatTime && !isGreatTiming)
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
            onTrickPerformed.Raise(this, isPerfectTiming);
        }
        else if (isGreatTiming)
        {
            onTrickPerformed.Raise(this, "Great");
        }
        //Depurar, añadir tiempo de cooldown y isKeepTrick por isStateTrick???
        else if (onCombo && !trick.isStateTrick) 
        {
            ResetCombo();
            return;
        }
        onTrickPerformed.Raise(this, trick);

        tricksPerformed.Add(trick);      
        onCombo = true;

        //Times
        trickCooldownTime = trick.listenInputTime; //El tiempo en el que el jugador puede pulsar el input para hacer el siguiente truco
        trickCooldownTimer = trickCooldownTime + listenInputOffset; //El tiempo total que tiene el jugador para hacer el siguiente truco, incluyendo offset general a lo coyote time
        trickPerfectTime = trickCooldownTime * trickPerfectTimingPercentage; //El momento a partir del cual el jugador tiene un timing perfecto para hacer el siguiente truco
        trickGreatTime = trickCooldownTime * trickGreatTimingPercentage;
                      
        SetAvailableTricks(trick.comboTricks);        
    }  
    void EndCombo(bool reset) //Cuando es true quiere decir que has fallado el combo
    {
        SetAvailableTricks(baseTricks);
        if (reset)
        {
            //lastTrickPerformed = null;
            onCombo = false;
            trickCooldownTimer = trickCooldownTime;
        }      
            
        //trickCooldownTimer = trickCooldownTime;

        onComboEnd.Raise(this, reset);       
    }
    private void ResetCombo()
    {
        Debug.Log("Combo reset");
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
                onTrickPerformed.Raise(this, wallSlideTrick);
            }
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
            if (!isOnWall)
            {
                //wallScoreTimer = 0f;
                SetAvailableTricks(baseTricks);                
                onTrickPerformed.Raise(this, wallSlideTrick.listenInputTime);
                                
            }
            else
            {               
                PerformTrick(wallSlideTrick);
            }
            
        }
    }
    public void HandleOnWallJump(Component sender, object data)
    {
        if(data is null)
        {
            PerformTrick(wallJumpTrick);
        }
    }

    public void HandleOnWallCharge(Component sender, object data)
    {
        if(data is null)
        {            
            PerformTrick(wallChargeTrick);
        }
    }

}
