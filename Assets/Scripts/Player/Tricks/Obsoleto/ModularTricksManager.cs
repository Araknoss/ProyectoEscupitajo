using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModularTricksManager : MonoBehaviour
{
    //List<Trick> combo;
    //float lastInputTime;
    //float lastComboEnd;
    //int comboCounter;
    //[SerializeField] private float bufferTime=0.5f;
    //[SerializeField] private float delayToTrick = 0.2f;
    //[SerializeField] private float endComboTime=1f;

    //[SerializeField] private Animator animator;
    
    
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.U))
    //    {
    //        DoTrick();
    //    }
    //    ExitTrick();
    //}

    //void DoTrick()
    //{
    //    if(Time.time-lastComboEnd>bufferTime&& comboCounter <= combo.Count)
    //    {
    //        CancelInvoke("EndCombo");

    //        if (Time.time - lastInputTime > delayToTrick)
    //        {
    //            animator.runtimeAnimatorController=combo[comboCounter].animatorOV;
    //            animator.Play("Trick", 0, 0);
    //            //Añadir puntos y gamefeel
    //            comboCounter++;
    //            lastInputTime = Time.time;

    //            if(comboCounter > combo.Count)
    //            {
    //                comboCounter = 0;
    //            }
    //        }
    //    }
    //}

    //void ExitTrick()
    //{
    //    AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
    //    if (info.normalizedTime > 0.9 && info.IsTag("Trick")) //Si la animación ha superado el 90% y tiene tag Trick
    //    {
    //        Invoke("EndCombo", endComboTime);
    //    }
    //}

    //void EndCombo()
    //{
    //    comboCounter = 0;
    //    lastComboEnd = Time.time;
    //}
}
