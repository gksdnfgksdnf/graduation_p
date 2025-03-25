using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DecisionDisplayer : MonoBehaviour
{
    public List<DecisionButton> buttons;

    public Action<Decision> onSelected;

    public void Init()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].Init();
            buttons[i].Inactive();
        }
    }

    public void Show(List<Decision> decisions)
    {
        for (int i = 0; i < decisions.Count; i++)
            buttons[i].Active(decisions[i], Select);
    }

    public void Unshow()
    {
        for (int i = 0; i < buttons.Count; i++)
            buttons[i].Inactive();
    }

    public void Select(Decision decision)
    {
        onSelected.Invoke(decision);
        Unshow();
    }
}
