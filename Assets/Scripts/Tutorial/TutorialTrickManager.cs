using Rewired;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TutorialTrickManager : MonoBehaviour
{
    [SerializeField] private List<Trick> tutorialTricks = new List<Trick>();
    [SerializeField] private int tutorialTrickIndex = 0;

    [SerializeField] private List<Trick> tricksPerformed = new List<Trick>();
    [SerializeField] private List<Trick> availableTricks = new List<Trick>();
    [SerializeField] private List<Trick> baseTricks = new List<Trick>();
    [SerializeField] private List<Trick> wallBaseTricks = new List<Trick>();
    private Trick lastTrickPerformed;

    [Header("Input")]
    [SerializeField] private int playerId;
    [SerializeField] private Player rewiredPlayer;

    //[SerializeField] private KeyCode bodyKey= KeyCode.J;
    [SerializeField] private int bodyTrickActionId;
    private bool bodyInput;
    //[SerializeField] private KeyCode skateKey= KeyCode.K;
    [SerializeField] private int skateTrickActionId;
    private bool skateInput;
    //[SerializeField] private KeyCode keepKey= KeyCode.Space;    
    [SerializeField] private int keepKeyActionId;
    private bool keepInputPress;
    private bool keepInputRelease;

    [Header("Variables")]
    [SerializeField] private float trickCooldownTime = 0.2f; //Este tiempo depende de cada truco
    [SerializeField] private float trickCooldownTimer;
    [SerializeField] private float trickAnimationTimer;
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
    //[SerializeField] private float wallScoreTime = 0.1f;
    public bool isOnWall = false;
    private bool isOnWallSlide = false;
    //private bool isOnWallCharge=false;
    //private float wallScoreTimer;        

    [Header("Events")]
    public GameEvent onTrickPerformed;
    public GameEvent onTrickPerformedOnPerfectTiming;
    public GameEvent onTrickPerformedOnGreatTiming;
    public GameEvent onKeepingTrick;
    //public GameEvent onWallSlidePerformed;
    public GameEvent onWallSlideEnd;
    public GameEvent onAvailableTricksReset;
    public GameEvent onComboEnd;
    public GameEvent onPerfectTiming;
    public GameEvent onGreatTiming;
    public GameEvent onTrickAnimationEnd;

    public GameEvent tutorialFirstTrickPerformed;
    public GameEvent tutorialSecondTrickPerformed;
    public GameEvent tutorialThirdTrickPerformed;

    [Header("Debug")]
    [SerializeField] private Animator animator;

    private void Awake()
    {
        rewiredPlayer = ReInput.players.GetPlayer(playerId);
    }
    private void Start()
    {        
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
        HandleTrickAnimationTimer();
        //HandleBodyTricks();
    }
    void InitializeInput()
    {
        bodyInput = rewiredPlayer.GetButtonDown("BodyTrick");
        skateInput = rewiredPlayer.GetButtonDown("SkateTrick");
        keepInputPress = rewiredPlayer.GetButtonDown("KeepTrick");
        keepInputRelease = rewiredPlayer.GetButtonUp("KeepTrick");
    }
    void HandleInput() 
    {
        if (tutorialTrickIndex == 0 && bodyInput)
        {
            PerformTrick(tutorialTricks[0]);
            Debug.Log("Tutorial Trick performed: " + tutorialTricks[0].trickName);
        }
        if(tutorialTrickIndex == 1 && skateInput)
        {
            PerformTrick(tutorialTricks[1]);
            Debug.Log("Tutorial Trick performed: " + tutorialTricks[1].trickName);
        }
            //if (bodyInput && !onKeepTrick)
            //{
            //    CheckAvailable(bodyTrickActionId);
            //}
            //if (skateInput && !onKeepTrick)
            //{
            //    CheckAvailable(skateTrickActionId);
            //}
            //if (keepInputPress && !onKeepTrick)
            //{
            //    CheckAvailable(keepKeyActionId);
            //}
            //if (keepInputRelease && onKeepTrick)
            //{
            //    onKeepTrick = false;
            //    if (!UnlockablesManager.Instance.HasUnlockedTrick(availableTricks[0]))
            //    {
            //        PerformTrick(baseTricks[1]);
            //        return;
            //    }
            //    PerformTrick(availableTricks[0]); //Se asume que solo hay un truco al soltar           
            //}
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
        else if (onCombo) //Cuando el cooldown termina se resetean los trucos disponibles
        {
            ResetCombo();
        }
    }

    void HandleTrickAnimationTimer()
    {
        if (trickAnimationTimer > 0f)
        {
            trickAnimationTimer -= Time.deltaTime;
        }
        else
        {
            trickAnimationTimer = trickCooldownTime;
            onTrickAnimationEnd.Raise(this, null);
        }
    }

    void SetAvailableTricks(List<Trick> newAvailableTricks)
    {
        availableTricks.Clear();

        for (int i = 0; i < newAvailableTricks.Count; i++)
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

    public void InitializeAvailableTricks()
    {
        tutorialTrickIndex = 0;
        availableTricks.Clear();
        availableTricks.Add(tutorialTricks[tutorialTrickIndex]);
        onAvailableTricksReset.Raise(this, availableTricks);
    }
    public void SetTutorialTrickAvailable()
    {
        if (tutorialTrickIndex < tutorialTricks.Count)
        {
            tutorialTrickIndex++;
            availableTricks.Clear();
            availableTricks.Add(tutorialTricks[tutorialTrickIndex]);
            onAvailableTricksReset.Raise(this, availableTricks);
            //PerformTrick(tutorialTricks[tutorialTrickIndex]);            
        }
    }

    void PerformTrick(Trick trick)
    {
        if (isPerfectTiming)
        {
            onTrickPerformedOnPerfectTiming.Raise(this, isPerfectTiming);
        }
        else if (isGreatTiming)
        {
            onTrickPerformedOnGreatTiming.Raise(this, isGreatTiming);
        }
        //else if (onCombo && !trick.isStateTrick)
        //{
        //    ResetCombo();
        //    return;
        //}
        onTrickPerformed.Raise(this, trick);

        Debug.Log("Tutorial Trick performed: " + trick.trickName);
        tricksPerformed.Add(trick);
        onCombo = true;

        //Times
        ResetTimes(trick);

        lastTrickPerformed = trick;
        if (trick.isKeepTrick)
        {
            onKeepTrick = true;
        }
        if (isOnWallSlide && trick != wallSlideTrick)
        {
            WallSlideEnd();
        }
    }

    void PerformWallSlideTrick() //Se ejecuta al entrar en contacto con la pared 1 vez
    {
        onTrickPerformed.Raise(this, wallSlideTrick);
        lastTrickPerformed = wallSlideTrick;
        SetAvailableTricks(wallSlideTrick.comboTricks);
    }

    void WallSlideEnd() //Se ejecuta al salir de la pared o al hacer un wall jump o wall charge
    {
        //onWallSlideEnd.Raise(this, wallSlideTrick);
        //ResetTimes(wallSlideTrick);
        //SetAvailableTricks(baseTricks);

        //isOnWallSlide = false;
    }

    private void ResetTimes(Trick trick)
    {
        trickCooldownTime = trick.listenInputTime; //El tiempo en el que el jugador puede pulsar el input para hacer el siguiente truco        
        trickAnimationTimer = trickCooldownTime;
        trickCooldownTimer = trickCooldownTime + listenInputOffset; //El tiempo total que tiene el jugador para hacer el siguiente truco, incluyendo offset general a lo coyote time
        trickPerfectTime = trickCooldownTime * trickPerfectTimingPercentage; //El momento a partir del cual el jugador tiene un timing perfecto para hacer el siguiente truco
        trickGreatTime = trickCooldownTime * trickGreatTimingPercentage;
    }

    private void ResetCombo()
    {
        SetAvailableTricks(baseTricks);

        onCombo = false;
        onKeepTrick = false;

        trickCooldownTimer = trickCooldownTime;
        onComboEnd.Raise(this, true);
    }
    void HandleKeepTrick()
    {
        if (keepInputPress)
        {
            performKeepTrickTimer = 0;
        }
        performKeepTrickTimer += Time.deltaTime;
        if (performKeepTrickTimer > performKeepTrickTime)
        {
            performKeepTrickTimer = 0;
            if (isOnWallSlide)
            {
                onKeepingTrick.Raise(this, wallSlideTrick);
                return;
            }
            onKeepingTrick.Raise(this, lastTrickPerformed);
            Debug.Log("KeepTrick performed");
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
            isOnWallSlide = (bool)data;
            onKeepTrick = false;

            if (!isOnWall /*&& lastTrickPerformed == wallSlideTrick*/) //Asi solo se activa cuando sales de la pared sin usar trucos
            {
                //SetAvailableTricks(baseTricks);
                //WallSlideEnd();
                if (lastTrickPerformed == wallSlideTrick)
                {
                    WallSlideEnd();
                }

            }
            else //Cuando entras en contacto se triggerea el truco
            {
                PerformWallSlideTrick();
            }

        }
    }
    public void HandleOnWallJump(Component sender, object data)
    {
        if (data is bool)
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
        if (data is bool)
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

}
