using Cysharp.Threading.Tasks;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public async UniTask PlayDialogue(DialogueHeader dialogue)
    {
        DialogueObject next = dialogue.header;
        while (next != null)
        {
            var customer = CustomerManager.Instance.FindVisitor(next.type);
            customer.Displayer.Enable(true);
            DialogueType last = next.type;
            next = await next.Dialogue(customer);
            if (next == null || next.type != last)
                customer.Displayer.Enable(false);
        }
    }
}
