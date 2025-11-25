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
    }
    private void CheckInputs()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
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

