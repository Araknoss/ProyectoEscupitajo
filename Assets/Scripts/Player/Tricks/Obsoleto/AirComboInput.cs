using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirComboInput : MonoBehaviour
{
    [Header("State")]
    [SerializeField] private bool isInWall;

    [Header("Inputs")]
    [SerializeField] private KeyCode grabKey = KeyCode.J;
    [SerializeField] private KeyCode flipKey = KeyCode.K;
    [SerializeField] private KeyCode spinKey = KeyCode.L;
    [SerializeField] private float directionThreshold = 0.5f;

    [Header("Wall Tricks")]
    [SerializeField] private Trick wallStaticTrick;

    [Header("Grab Tricks")]
    [SerializeField] private Trick neutralGrabTrick;
    [SerializeField] private Trick upGrabTrick;
    [SerializeField] private Trick downGrabTrick;
    [SerializeField] private Trick leftGrabTrick;
    [SerializeField] private Trick rightGrabTrick;

    [Header("Flip Tricks")]
    [SerializeField] private Trick neutralFlipTrick;
    [SerializeField] private Trick upFlipTrick;
    [SerializeField] private Trick downFlipTrick;
    [SerializeField] private Trick leftFlipTrick;
    [SerializeField] private Trick rightFlipTrick;

    [Header("Spin Trick")]
    [SerializeField] private Trick spinTrick;

    [Header("Components")]
    [SerializeField] private ComboManager comboManager;

    private float xInput;
    private float yInput;

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

        if (isInWall)
        {
            HandleWallInput();
            return;
        }
        
        HandleGrabInput();
        HandleFlipInput();
        HandleSpinInput();
    }

    private void CheckInputs()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
    }

    private void HandleWallInput()
    {
        if (!Input.GetKeyDown(grabKey))
        {
            return;
        }
        if (wallStaticTrick != null)
        {
            comboManager.AddTrick(wallStaticTrick);
        }
    }
    private void HandleGrabInput()
    {
        if (!Input.GetKeyDown(grabKey))
        {
            return;
        }

        Trick trickToAdd = neutralGrabTrick;

        if (yInput > directionThreshold && upGrabTrick != null)
        {
            trickToAdd = upGrabTrick;
        }
        else if (yInput < -directionThreshold && downGrabTrick != null)
        {
            trickToAdd = downGrabTrick;
        }
        else if (xInput > directionThreshold && rightGrabTrick != null)
        {
            trickToAdd = rightGrabTrick;
        }
        else if (xInput < -directionThreshold && leftGrabTrick != null)
        {
            trickToAdd = leftGrabTrick;
        }

        comboManager.AddTrick(trickToAdd);
    }

    private void HandleFlipInput()
    {
        if (!Input.GetKeyDown(flipKey))
        {
            return;
        }

        Trick trickToAdd = neutralFlipTrick;

        if (yInput > directionThreshold && upFlipTrick != null)
        {
            trickToAdd = upFlipTrick;
        }
        else if (yInput < -directionThreshold && downFlipTrick != null)
        {
            trickToAdd = downFlipTrick;
        }
        else if (xInput > directionThreshold && rightFlipTrick != null)
        {
            trickToAdd = rightFlipTrick;
        }
        else if (xInput < -directionThreshold && leftFlipTrick != null)
        {
            trickToAdd = leftFlipTrick;
        }

        comboManager.AddTrick(trickToAdd);
    }

    private void HandleSpinInput()
    {
        if (!Input.GetKeyDown(spinKey))
        {
            return;
        }

        if (spinTrick == null)
        {
            return;
        }

        comboManager.AddTrick(spinTrick);
    }

    public void SetIsInWall(bool value)
    {
        isInWall = value;
    }
}

