using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public DialogueManager Instance { get; private set; }

    [SerializeField] private DialogueSO testDialogue;

    private void Awake()
    {
        Instance = this;
        Debug.Log(JsonUtility.ToJson(testDialogue));
    }

    public void EnterDialogue(DialogueSO dialogue)
    {

    }
}
