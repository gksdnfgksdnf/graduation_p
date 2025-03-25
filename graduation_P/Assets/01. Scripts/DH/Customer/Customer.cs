using Cysharp.Threading.Tasks;
using UnityEngine;

public enum BehaviourType
{
    None,
    Enter,
    Talk,
    Order,
    Reaction,
    Exit,
}

public abstract class Customer : MonoBehaviour
{
    public DialogueType type;

    public CustomerAnimator Animator;
    public CustomerDialogues Dialogues;
    public CustomerInformation Information;
    public CustomerTaste Taste;
    public TextDisplayer Displayer;

    protected virtual void Awake()
    {
        Dialogues.Initialize();
        Displayer.Init();
        Exit();
    }

    public virtual async UniTask Enter()
    {
        Animator.Enter();
        await UniTask.Delay(10);
    }

    public virtual void Exit()
    {
        Animator.Exit();
    }

    public virtual DialogueHeader Dialogue()
    {
        return Dialogues.Query(Information.drunk, Information.reliance);
    }

    public virtual void AddDecision(Decision decision)
    {
        foreach (var effect in decision.effects)
            Information.AddEffect(effect.type, effect.value);
    }

    public abstract bool Visit(int day, DayPhase phase);
}
