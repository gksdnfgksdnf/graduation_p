using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public enum DialogueType
    {
        Player,
        Customer
    }

    public static DialogueManager Instance { get; private set; }

    public bool test = false;
    [SerializeField] private Customer testCustomer;
    [SerializeField] private DialogueText testDialogue;

    public List<DialogueDisplayer> playerDisplayers;
    public List<DialogueDisplayer> customerDisplayers;

    private Customer customer;
    private DialogueText dialogue;
    private DialogueDisplayer displayer;

    private int dialoguePointer = 0;
    private DialogueText lastDialogue;

    public Action onDialogueEnded;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (test)
            EnterDialogue(testCustomer, testDialogue);
    }

    public void EnterDialogue(Customer customer, DialogueText dialogue)
    {
        dialoguePointer = 0;
        lastDialogue = this.dialogue;

        this.customer = customer;
        this.dialogue = dialogue;

        if (displayer != null)
        {
            displayer.Enable(false);
            displayer.TextDisplayer.nextButton.onClick.RemoveListener(Next);
        }

        if (displayer == null || dialogue.type != lastDialogue.type)
            displayer = GetDisplayer(dialogue);

        displayer.Enable(true);
        displayer.TextDisplayer.nextButton.onClick.AddListener(Next);
        displayer.TextDisplayer.Show(dialogue.texts[dialoguePointer++]);
    }

    private DialogueDisplayer GetDisplayer(DialogueText dialogue)
    {
        switch (dialogue.type)
        {
            case DialogueType.Player:
                return playerDisplayers[UnityEngine.Random.Range(0, playerDisplayers.Count)];
            case DialogueType.Customer:
                return customerDisplayers[UnityEngine.Random.Range(0, customerDisplayers.Count)];
        }
        return null;
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

        onDialogueEnded?.Invoke();
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
            if (dialogue.decisions == null || dialogue.decisions.Count > 0)
            {
                ShowDecisions();
                return;
            }

            if (dialogue.next != null)
            {
                EnterDialogue(customer, dialogue.next);
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
        displayer.DecisionDisplayer.Show(customer, dialogue.decisions);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Next();
        }
    }
}
