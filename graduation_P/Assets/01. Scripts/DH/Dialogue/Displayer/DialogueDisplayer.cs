using UnityEngine;

public class DialogueDisplayer : MonoBehaviour
{
    public TextDisplayer TextDisplayer;
    public DecisionDisplayer DecisionDisplayer;
    [SerializeField] private CanvasGroup canvasGroup;

    private void Awake()
    {
        if (TextDisplayer == null)
            TextDisplayer = GetComponentInChildren<TextDisplayer>();
        TextDisplayer.Init();

        if (DecisionDisplayer == null)
            DecisionDisplayer = GetComponentInChildren<DecisionDisplayer>();
        DecisionDisplayer.Init();

        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();

        Enable(false);
    }

    public void Enable(bool enable)
    {
        canvasGroup.interactable = enable;
        canvasGroup.blocksRaycasts = enable;
        canvasGroup.alpha = enable ? 1f : 0f;

        TextDisplayer.Enable(enable);
        DecisionDisplayer.Unshow();
    }
}
