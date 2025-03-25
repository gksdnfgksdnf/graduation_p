using System.Collections;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void PlayDialogue(DialogueHeader dialogue, EventWaiter waiter)
    {
        StartCoroutine(Play(dialogue, waiter));
    }

    private IEnumerator Play(DialogueHeader dialogue, EventWaiter waiter)
    {
        DialogueObject next = dialogue.header;
        DialogueObject last = null;
        DialogueEventWaiter waitDialogue = new();
        while (next != null)
        {
            var customer = CustomerManager.Instance.FindVisitor(next.type);
            if (last == null || next.type != last.type)
                customer.Displayer.Enable(true);
            last = next;

            waitDialogue.IsCompleted = false;
            waitDialogue.next = null;

            next.Dialogue(customer, waitDialogue);
            yield return new WaitUntil(() => waitDialogue.IsCompleted);
            next = waitDialogue.next;

            if (next == null || next.type != last.type)
                customer.Displayer.Enable(false);
        }
        waiter.IsCompleted = true;
    }
}
