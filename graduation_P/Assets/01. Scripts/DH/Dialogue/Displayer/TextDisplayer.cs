using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textBase;
    [SerializeField] private Button nextButton;
    [SerializeField] private Canvas canvas;
    [SerializeField] private CanvasGroup group;

    public float textShowInterval = 0.1f;

    private Coroutine coroutine;

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
    public bool IsShowComplete() => coroutine == null;
    public void Enable(bool enable)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }

        textBase.text = "";

        group.alpha = enable ? 1 : 0;
        group.interactable = enable;
        group.blocksRaycasts = enable;
    }

    public void Show(string message)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }

        textBase.text = message;
        coroutine = StartCoroutine(ShowText());
    }

    public void Skip()
    {
        ShowAllText();
    }
    private IEnumerator ShowText()
    {
        int textMax = textBase.text.Length;

        textBase.ForceMeshUpdate();
        textBase.maxVisibleCharacters = 0;

        var wait = new WaitForSeconds(textShowInterval);
        for (int i = 0; i < textMax; i++)
        {
            yield return wait;
            textBase.ForceMeshUpdate();
            textBase.maxVisibleCharacters++;
        }
        coroutine = null;
    }
    private void ShowAllText()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }

        textBase.maxVisibleCharacters = textBase.text.Length;
    }
}
