using UnityEngine;

public class CustomerAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private new SpriteRenderer renderer;

    public Animator Animator => animator;

    public void Enter()
    {
        renderer.enabled = true;
    }

    public void Exit()
    {
        renderer.enabled = false;
    }

    public void SetBool(int animationHash, bool active)
    {
        animator.SetBool(animationHash, active);
    }
    public void SetBool(string animationName, bool active)
    {
        animator.SetBool(animationName, active);
    }
    public void SetTrigger(int animationHash)
    {
        animator.SetTrigger(animationHash);
    }
    public void SetTrigger(string animationName)
    {
        animator.SetTrigger(animationName);
    }
}
