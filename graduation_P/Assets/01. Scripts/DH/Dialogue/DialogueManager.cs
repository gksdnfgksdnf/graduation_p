using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [SerializeField] private Customer testCustomer;
    [SerializeField] private DialogueText testDialogue;
    [SerializeField] private DialogueDisplayer testDisplayer;

    private Customer customer;
    private DialogueText dialogue;
    private DialogueDisplayer displayer;

    private int dialoguePointer = 0;

    // public event Action OnDialogueEnd;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        EnterDialogue(testCustomer, testDisplayer, testDialogue);
    }

    public void EnterDialogue(Customer customer, DialogueDisplayer displayer, DialogueText dialogue)
    {
        this.customer = customer;
        this.dialogue = dialogue;
        dialoguePointer = 0;

        if (this.displayer != null)
        {
            this.displayer.Enable(false);
            this.displayer.TextDisplayer.nextButton.onClick.RemoveListener(Next);
        }

        this.displayer = displayer; // displayers[UnityEngine.Random.Range(0, displayers.Count)];
        this.displayer.Enable(true);
        this.displayer.TextDisplayer.nextButton.onClick.AddListener(Next);
        this.displayer.TextDisplayer.Show(dialogue.texts[dialoguePointer++]);
    }

    public void ExitDialogue()
    {
        customer = null;
        dialogue = null;
        dialoguePointer = 0;

        if (displayer != null)
        {
            displayer.TextDisplayer.nextButton.onClick.RemoveListener(Next);
            displayer.Enable(false);
            displayer = null;
        }
    }

    public void Next() // 화면 클릭 작용, 나오는 중 누르면 줄 스킵, 끝나면 선택지나 대화 종료 중에 결정
    {
        if (!displayer)
            return;
        if (displayer.DecisionDisplayer.IsShowDecisions())
            return;
        if (!displayer.TextDisplayer.IsShowComplete())
        {
            displayer.TextDisplayer.Skip();
            return;
        }
        if (dialoguePointer == dialogue.texts.Count)
        {
            if (dialogue.decisions.Count > 0)
            {
                ShowDecisions();
                return;
            }

            ExitDialogue();
            return;
        }

        displayer.TextDisplayer.Show(dialogue.texts[dialoguePointer++]);
    }

    public void Skip()
    {
        displayer.TextDisplayer.Skip();
    }

    private void ShowDecisions()
    {
        displayer.DecisionDisplayer.Show(customer, displayer, dialogue.decisions);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Next();
        }
    }
}
