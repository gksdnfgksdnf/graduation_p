using UnityEngine;

public enum BehaviourType
{
    None,
    Enter,
    Talk,
    Question,
    Answer,
    Order,
    Reaction,
    Exit,
}

public class CustomerBehaviour
{
    public BehaviourType type;
    public DialogueObject dialogue;
}

public class Customer : MonoBehaviour
{
    public CustomerAnimator Animator;
    public CustomerDialogue Dialogue;
    public CustomerDialogues Dialogues;
    public CustomerInfomation Infomation;
    public CustomerTaste Taste;

    public virtual void Enter()
    {
        Animator.Enter();
    }

    public virtual void Exit()
    {
        Animator.Exit();
    }

    public virtual void Talk()
    {

    }

    public virtual void Question()
    {

    }
    public virtual void Answer(Customer from, DialogueDecision decision)
    {

    }

    public virtual void Order()
    {
    }

    public virtual void Serve(CocktailDataSO cocktail)
    {
    }
}
