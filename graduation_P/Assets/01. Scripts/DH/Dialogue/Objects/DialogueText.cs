using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "DialogueText")]
public class DialogueText : DialogueObject // default text dialogue
{
    public List<string> texts;
    public DialogueObject next;

    private TextDisplayer displayer;

    public override async UniTask<DialogueObject> Dialogue(Customer customer)
    {
        Debug.Log("Enter Texts");

        displayer = customer.Displayer;
        foreach (string text in texts)
        {
            await displayer.Show(text);

            bool wait = false;
            UnityAction waitNext = () => wait = true;
            displayer.onClick.AddListener(waitNext);
            await UniTask.WaitUntil(() => wait);
            displayer.onClick.RemoveListener(waitNext);
        }
        displayer = null;

        return next;
    }
}
