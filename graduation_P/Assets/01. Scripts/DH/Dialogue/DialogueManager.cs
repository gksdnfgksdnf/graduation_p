using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public enum SpeakerType
    {
        Player,
        Customer
    }

    public static DialogueManager Instance { get; private set; }

    public bool test = false;
    [SerializeField] private Customer testCustomer;
    [SerializeField] private DialogueObject testDialogue;

    public List<DialogueDisplayer> playerDisplayers;
    public List<DialogueDisplayer> customerDisplayers;

    private Customer customer;
    private DialogueObject dialogue;
    private DialogueObject lastDialogue;
    private DialogueDisplayer displayer;
    private DialogueDisplayer lastDisplayer;

    public Action onNext;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (test)
            EnterDialogue(testCustomer, testDialogue);
    }

    public void EnterDialogue(Customer customer, DialogueObject dialogue, DialogueDisplayer displayer = null)
    {
        this.customer = customer;
        this.lastDialogue = this.dialogue;
        this.dialogue = dialogue;
        this.lastDisplayer = displayer;
        this.displayer = displayer != null && dialogue.type == lastDialogue.type ? displayer : GetDisplayer(dialogue);
        if (lastDisplayer != this.displayer)
        {
            lastDisplayer?.Enable(false);
            this.displayer.Enable(true);
        }

        lastDialogue?.ExitDialogue(this.customer, this.displayer);
        this.dialogue?.EnterDialogue(this.customer, this.displayer);
    }

    private DialogueDisplayer GetDisplayer(DialogueObject dialogue)
    {
        switch (dialogue.type)
        {
            case SpeakerType.Player:
                return playerDisplayers[UnityEngine.Random.Range(0, playerDisplayers.Count)];
            case SpeakerType.Customer:
                return customerDisplayers[UnityEngine.Random.Range(0, customerDisplayers.Count)];
        }
        return null;
    }

    public void ExitDialogue()
    {
        lastDialogue = dialogue;
        dialogue?.ExitDialogue(customer, displayer);
        dialogue = null;
        customer = null;
        displayer.Enable(false);
        displayer = null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            onNext?.Invoke();
    }
}
