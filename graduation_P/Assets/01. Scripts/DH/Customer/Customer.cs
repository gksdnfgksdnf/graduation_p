using System.Collections;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public CustomerAI AI;
    public CustomerAnimator Animator;
    public AIBehaviour curr;
    public AIBehaviour prev;
    public float behaviourTime = 2f;

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
        curr = behaviour;
        DialogueManager.Instance.EnterDialogue(this, behaviour.dialogue.header);
        DialogueManager.Instance.onExit += HandleBehaviourEnd;
    }

    public void HandleBehaviourEnd()
    {
        DialogueManager.Instance.onExit -= HandleBehaviourEnd;
        prev = curr;
        curr = null;
        StartCoroutine(WaitNextBehaviour());
    }

    private IEnumerator WaitNextBehaviour()
    {
        yield return new WaitForSeconds(behaviourTime + Random.Range(0f, 1f));
        PlayNextBehaviour();
    }

    public void PlayNextBehaviour()
    {

        switch (prev.behaviour)
        {
            case BehaviourType.Enter:
                PlayBehaviour(AI.GetBehaviour());
                break;
            case BehaviourType.Talk:
                PlayBehaviour(AI.GetBehaviour());
                break;
            case BehaviourType.Order:

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
