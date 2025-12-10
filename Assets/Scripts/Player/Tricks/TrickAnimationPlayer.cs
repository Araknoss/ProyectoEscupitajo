using UnityEngine;

public class TrickAnimationPlayer : MonoBehaviour
{
    [SerializeField] private Animator animator;    

    public void HandleTrickPerformed(Component sender, object data)
    {
        if (data is not Trick) return;
        Trick trick = (Trick)data;        
        animator.Play(trick.animationClip.name,0,0f);
    }
}

