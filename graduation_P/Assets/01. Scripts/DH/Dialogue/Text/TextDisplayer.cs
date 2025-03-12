using System.Collections;
using TMPro;
using UnityEngine;

public class TextDisplayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textBase;
    private Coroutine coroutine;

    public float textShowInterval = 0.1f;

    public void Display(string message)
    {
        if (coroutine != null)
            StopCoroutine(coroutine);

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
    }

    private void ShowAllText()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);

        textBase.maxVisibleCharacters = textBase.text.Length;
    }

    private void Update()
    {

    }
}
