using UnityEngine;

public class Customer : MonoBehaviour
{
    public CustomerAI AI;
    public CustomerAnimator Animator;

    public void Enter()
    {
        AI.Entered();
        Animator.Enter();
        PlayBehaviour(AI.GetBehaviour());
    }

    public void Exit()
    {
        AI.Exited();
        Animator.Exit();
    }

    public void PlayBehaviour(AIBehaviour behaviour)
    {
        DialogueManager.Instance.EnterDialogue(this, behaviour.dialogue.header);
    }
}
