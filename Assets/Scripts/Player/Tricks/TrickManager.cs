using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrickManager : MonoBehaviour
{
    [SerializeField] private List<Trick> tricksPerformed = new List<Trick>();
    [SerializeField] private List<Trick> availableTricks = new List<Trick>();
    [SerializeField] private List<Trick> baseTricks = new List<Trick>();    

    [Header("Input")]
    [SerializeField] private KeyCode bodyKey= KeyCode.J;
    private bool bodyInput;
    [SerializeField] private KeyCode skateKey= KeyCode.K;
    private bool skateInput;

    [Header("Variables")]
    [SerializeField] private float trickCooldownTime=0.2f;
    private float trickCooldownTimer;
    private bool trickPerformed;
    [SerializeField] private float listenInputOffset;

    [Header("OnWall")]
    [SerializeField] private Trick wallSlideTrick;
    [SerializeField] private Trick wallJumpTrick;
    [SerializeField] private float wallScoreTime = 0.1f;
    private bool isOnWall=false;
    private float wallScoreTimer;    

    //[Header("Tricks")]
    //[SerializeField] private Trick horizontalFlip;
    //[SerializeField] private Trick verticalFlip;

    [Header("Events")]
    public GameEvent onTrickPerformed;
    public GameEvent onAvailableTricksReset;

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
    }
    void CheckAvailable(KeyCode input) //Se comprueba si el input corresponde a algun truco disponible
    {
        if(availableTricks.Any())
        {
            for(int i=0;i<availableTricks.Count;i++)
            {
                if(availableTricks[i].inputKey == input)
                {
                    PerformTrick(availableTricks[i]);
                    return;
                }
            }
        }        
    }    
    void HandleTrickCooldown()
    {
        if (trickCooldownTimer > 0f && trickPerformed)
        {
            trickCooldownTimer -= Time.deltaTime;
        }
        else if(trickPerformed) //Cuando el cooldown termina se resetean los trucos disponibles
        {           
            trickPerformed = false;
            SetAvailableTricks(baseTricks);
            trickCooldownTimer = trickCooldownTime;
        }
    }

    void SetAvailableTricks(List<Trick> newAvailableTricks)
    {
        availableTricks.Clear();        

        for (int i=0;i< newAvailableTricks.Count;i++)
        {
            if (UnlockablesManager.Instance.HasUnlockedTrick(newAvailableTricks[i]))
            {
                availableTricks.Add(newAvailableTricks[i]);
            }            
        }
        onAvailableTricksReset.Raise(this, availableTricks);
    }

    void PerformTrick(Trick trick)
    {                
        onTrickPerformed.Raise(this, trick);
        tricksPerformed.Add(trick);
        trickPerformed = true;

        trickCooldownTime=trick.listenInputTime;
        trickCooldownTimer = trickCooldownTime + listenInputOffset;

        SetAvailableTricks(trick.comboTricks);        
    }

    void HandleWallSlide()
    {        
        if(isOnWall)
        {
            wallScoreTimer += Time.deltaTime;
            if (wallScoreTimer > wallScoreTime)
            {
                wallScoreTimer = 0;
                PerformTrick(wallSlideTrick);
            }
        }
    }

    public void SetIsOnWall(Component sender, object data)
    {
        if (data is bool)
        {
            isOnWall = (bool)data; 
            if(!isOnWall)
            {
                wallScoreTimer = 0f;
                SetAvailableTricks(baseTricks);
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

}
