using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textBase;

    public Button nextButton;
    public float textShowInterval = 0.1f;
    public Vector2 textPadding = new Vector2(30, 30);

    private Coroutine coroutine;
    private RectTransform rectTrm;

    private void Awake()
    {
        rectTrm = transform as RectTransform;
    }

    public bool IsShowComplete()
    {
        return coroutine == null;
    }

    public void Enable(bool enable)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }

        textBase.text = "";
    }

    public void Show(string message)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }

        textBase.text = message;
        rectTrm.sizeDelta = textBase.GetPreferredValues() + textPadding;
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
