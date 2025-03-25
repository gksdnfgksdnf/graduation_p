using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class DecisionDisplayer : MonoBehaviour
{
    public List<DecisionButton> buttons;

    private bool isShow = false;
    private Decision selected = null;

    public void Init()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].Init();
            buttons[i].Inactive();
        }
    }

    public bool IsShowDecisions()
    {
        return isShow;
    }

    public async UniTask<Decision> Show(List<Decision> decisions)
    {
        for (int i = 0; i < decisions.Count; i++)
            buttons[i].Active(decisions[i], Select);
        isShow = true;

        await UniTask.WaitWhile(() => isShow);

        Unshow();
        return selected;
    }

    public void Unshow()
    {
        for (int i = 0; i < buttons.Count; i++)
            buttons[i].Inactive();
        isShow = false;
    }

    public void Select(Decision decision)
    {
        selected = decision;
        isShow = false;
    }
}
