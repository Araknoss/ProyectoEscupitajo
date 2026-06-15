using UnityEngine;

public class TrickAnimationPlayer : MonoBehaviour
{
    [SerializeField] private Animator animator;    

    public void HandleTrickPerformed(Component sender, object data)
    {
        if (data is not Trick) return;
        Trick trick = (Trick)data;
        if (trick.animationClip != null)
        {
            animator.SetBool("PerformingTrick", true);
            animator.Play(trick.animationClip.name, 0, 0f);
        }
        
    }

    public void HandleComboEnd(Component sender, object data)
    {
        animator.SetBool("PerformingTrick", false);
    }

    public void HandleOnWallSlideEnd(Component sender, object data)
    {
        animator.SetBool("PerformingTrick", false);
    }

    public void HandleOnPlayerDeath(Component sender, object data)
    {        
        animator.SetBool("Idle", false);
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        animator.SetTrigger("Death");
        Debug.Log("Player Death Animation Triggered");
    }
}

