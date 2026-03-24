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
}

