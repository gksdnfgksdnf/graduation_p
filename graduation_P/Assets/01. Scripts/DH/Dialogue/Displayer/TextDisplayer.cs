using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TextDisplayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textBase;
    [SerializeField] private Button nextButton;
    [SerializeField] private Canvas canvas;
    [SerializeField] private CanvasGroup group;

    public UnityEvent onClick => nextButton.onClick;

    public float textShowInterval = 0.1f;

    private EventWaiter localWaiter = new EventWaiter();

    public void Init()
    {
        if (canvas == null)
            canvas = GetComponent<Canvas>();
        if (canvas.worldCamera == null)
            canvas.worldCamera = Camera.main;
        if (group == null)
            group = GetComponent<CanvasGroup>();
        Enable(false);
    }
    public void Enable(bool enable)
    {
        if (!localWaiter.IsCompleted)
            localWaiter.IsCompleted = true;

        textBase.text = "";
        group.alpha = enable ? 1 : 0;
        group.interactable = enable;
        group.blocksRaycasts = enable;
    }

    public void Skip()
    {
        if (!localWaiter.IsCompleted)
        {
            localWaiter.IsCompleted = true;
            textBase.ForceMeshUpdate();
            textBase.maxVisibleCharacters = textBase.text.Length;
        }
    }

    public EventWaiter Show(string message)
    {
        onClick.AddListener(Skip);
        textBase.text = message;
        localWaiter.IsCompleted = false;
        StartCoroutine(ShowText());
        return localWaiter;
    }

    private IEnumerator ShowText()
    {
        int textMax = textBase.text.Length;
        var wait = new WaitForSeconds(textShowInterval);

        textBase.ForceMeshUpdate();
        textBase.maxVisibleCharacters = 0;

        for (int i = 0; i < textMax && !localWaiter.IsCompleted; i++)
        {
            yield return wait;
            if (localWaiter.IsCompleted)
                yield break;
            textBase.ForceMeshUpdate();
            textBase.maxVisibleCharacters++;
        }

        onClick.RemoveListener(Skip);
        localWaiter.IsCompleted = true;
    }
}
