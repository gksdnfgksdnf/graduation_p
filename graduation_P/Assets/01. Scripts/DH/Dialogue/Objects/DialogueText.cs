using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "DialogueText")]
public class DialogueText : DialogueObject // default text dialogue
{
    public List<string> texts;
    public DialogueObject next;

    private Customer customer;
    private TextDisplayer displayer;
    private DialogueEventWaiter waiter;

    private EventWaiter localWaiter = new EventWaiter();

    public override void Dialogue(Customer customer, DialogueEventWaiter waiter)
    {
        this.customer = customer;
        this.displayer = customer.Displayer;
        this.waiter = waiter;

        localWaiter.IsCompleted = false;
        customer.StartCoroutine(Show());
    }

    private IEnumerator Show()
    {
        foreach (string text in texts)
        {
            if (localWaiter.IsCompleted)
                yield break;

            EventWaiter textWaiter = displayer.Show(text);
            yield return new WaitUntil(() => textWaiter.IsCompleted);

            bool waitClick = false;
            UnityAction clickAction = () => waitClick = true;
            displayer.onClick.AddListener(clickAction);
            yield return new WaitUntil(() => waitClick);
            displayer.onClick.RemoveListener(clickAction);
        }

        localWaiter.IsCompleted = true;

        waiter.next = next;
        waiter.IsCompleted = true;
    }

    public void SkipAll()
    {
        if (!localWaiter.IsCompleted)
        {
            localWaiter.IsCompleted = true;
            displayer.Show(texts[^1]);
            displayer.Skip();
            waiter.next = next;
            waiter.IsCompleted = true;
        }
    }
}
