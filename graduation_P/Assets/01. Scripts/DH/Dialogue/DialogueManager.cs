using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public DialogueManager Instance { get; private set; }

    [SerializeField] private DialogueText testDialogue;

    private Customer customer;
    private DialogueText dialogue;

    private void Awake()
    {
        Instance = this;
        Debug.Log(JsonUtility.ToJson(testDialogue));

        EnterDialogue(null, testDialogue);
    }

    public void EnterDialogue(Customer customer, DialogueText dialogue)
    {
        this.customer = customer;
        this.dialogue = dialogue;
    }

    public void Next()
    {

    }

    public void Skip()
    {

    }
}
