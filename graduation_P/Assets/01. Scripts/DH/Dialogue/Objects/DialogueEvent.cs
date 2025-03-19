using UnityEngine;

[CreateAssetMenu(menuName = "DialogueEvent")]
public class DialogueEvent : DialogueText
{
    public BehaviourType nextBehaviour;
    public SpecialEvent specialEvent;

    public override void ExitDialogue(Customer customer, DialogueDisplayer displayer)
    {
        base.ExitDialogue(customer, displayer);
        customer.AI.SetBehaviourType(nextBehaviour);
        customer.AI.SetSpecialEvent(specialEvent);
    }
}
