using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Dialogue/Text")]
public class DialogueText : DialogueObject // default text dialogue
{
    public List<string> texts;
    public DialogueObject next;

    private int textPtr = 0;

    private Customer customer;
    private DialogueDisplayer displayer;

    public override void EnterDialogue(Customer customer, DialogueDisplayer displayer)
    {
        this.customer = customer;
        this.displayer = displayer;

        textPtr = 0;
        DialogueManager.Instance.onNext += Next;
        displayer.TextDisplayer.Show(texts[textPtr++]);
    }

    private void Next()
    {
        if (!displayer.TextDisplayer.IsShowComplete())
            displayer.TextDisplayer.Skip();
        else if (textPtr == texts.Count)
        {
            if (next)
                DialogueManager.Instance.EnterDialogue(customer, next, displayer);
            else
                DialogueManager.Instance.ExitDialogue();
        }
        else
            displayer.TextDisplayer.Show(texts[textPtr++]);
    }

    public override void ExitDialogue(Customer customer, DialogueDisplayer displayer)
    {
        DialogueManager.Instance.onNext -= Next;
    }
}
