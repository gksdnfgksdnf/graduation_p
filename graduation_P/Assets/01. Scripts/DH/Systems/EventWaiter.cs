using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventWaiter
{
    public bool IsCompleted = false;

    public static IEnumerator WaitInvoker(Action<EventWaiter> handle)
    {
        if (handle == null)
            yield break;
        EventWaiter waiter = new EventWaiter();
        handle?.Invoke(waiter);
        yield return new WaitUntil(() => waiter.IsCompleted);
    }
    public static IEnumerator WaitInvoker(List<Action<EventWaiter>> handlers)
    {
        List<EventWaiter> waiters = new List<EventWaiter>();
        foreach (var handle in handlers)
        {
            if (handle == null)
                continue;
            EventWaiter waiter = new EventWaiter();
            handle?.Invoke(waiter);
            waiters.Add(waiter);
        }
        yield return new WaitUntil(() => waiters.All(w => w.IsCompleted));
    }
}

public class DialogueEventWaiter : EventWaiter
{
    public DialogueObject next = null;
}
