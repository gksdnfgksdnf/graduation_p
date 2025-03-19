using UnityEngine;

public class Customer : MonoBehaviour
{
    public CustomerAI AI;
    public CustomerAnimator Animator;
    public AIBehaviour current;

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

    public void Order(CocktailDataSO cocktail)
    {
        AI.OrderCocktail(cocktail);
    }

    public void Serve(CocktailDataSO cocktail)
    {
        AI.ServeCocktail(cocktail);
        PlayBehaviour(AI.GetBehaviour());
    }

    public AIBehaviour GetBehaviour()
    {
        return AI.GetBehaviour();
    }

    public void PlayBehaviour(AIBehaviour behaviour)
    {
        current = behaviour;
        DialogueManager.Instance.EnterDialogue(this, behaviour.dialogue.header);
        DialogueManager.Instance.onExit += HandleBehaviourEnd;
    }

    public void HandleBehaviourEnd()
    {
        switch (current.behaviour)
        {
            case BehaviourType.None:
                PlayBehaviour(AI.GetBehaviour());
                break;
            case BehaviourType.Enter:
                PlayBehaviour(AI.GetBehaviour());
                break;
            case BehaviourType.Talk:
                PlayBehaviour(AI.GetBehaviour());
                break;
            case BehaviourType.Reaction:
                PlayBehaviour(AI.GetBehaviour());
                break;
            case BehaviourType.Exit:
                Exit();
                break;
        }
    }
}
