using UnityEngine;

public class Customer : MonoBehaviour
{
    public CustomerAI AI;
    public CustomerAnimator Animator;

    public void Enter(EnterEventType evt)
    {
        AI.Entered(evt);
        Animator.Enter();
    }

    public void Exit()
    {
        AI.Exited();
        Animator.Exit();
    }
}
