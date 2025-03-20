using System;
using System.Collections.Generic;
using UnityEngine;

public class DecisionDisplayer : MonoBehaviour
{
    public List<DecisionButton> buttons;

    private bool isShow = false;

    public void Init()
    {
        for (int i = 0; i < buttons.Count; i++)
            buttons[i].Init();
    }

    public bool IsShowDecisions()
    {
        return isShow;
    }

    public void Show(DialogueDecision decision, Action<Decision> callback)
    {
        callback += Select;

        for (int i = 0; i < decision.decisions.Count; i++)
            buttons[i].Active(decision.decisions[i], callback);
        isShow = true;
    }

    public void Unshow()
    {
        for (int i = 0; i < buttons.Count; i++)
            buttons[i].Inactive();
        isShow = false;
    }

    public void Select(Decision decision)
    {
        Unshow();
    }
}
