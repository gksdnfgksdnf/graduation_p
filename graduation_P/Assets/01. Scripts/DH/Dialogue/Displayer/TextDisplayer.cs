using Cysharp.Threading.Tasks;
using System.Threading;
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

    public int textShowInterval = 100;

    private UniTask task;
    private CancellationTokenSource cancellation;

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
    public bool IsShowComplete() => task.Status == UniTaskStatus.Succeeded || cancellation.IsCancellationRequested;
    public void Enable(bool enable)
    {
        if (!IsShowComplete())
            cancellation.Cancel();

        textBase.text = "";
        group.alpha = enable ? 1 : 0;
        group.interactable = enable;
        group.blocksRaycasts = enable;
    }

    public void Skip()
    {
        if (IsShowComplete())
            return;

        cancellation.Cancel();
    }

    public async UniTask Show(string message)
    {
        onClick.AddListener(Skip);
        textBase.text = message;
        cancellation = new CancellationTokenSource();
        await ShowText(cancellation);
        onClick.RemoveListener(Skip);
    }

    private async UniTask ShowText(CancellationTokenSource cancellation)
    {
        int textMax = textBase.text.Length;

        textBase.ForceMeshUpdate();
        textBase.maxVisibleCharacters = 0;

        for (int i = 0; i < textMax; i++)
        {
            await UniTask.Delay(textShowInterval, cancellationToken: cancellation.Token);
            textBase.ForceMeshUpdate();
            if (cancellation.IsCancellationRequested)
            {
                textBase.maxVisibleCharacters = textBase.text.Length;
                return;
            }
            textBase.maxVisibleCharacters++;
        }
    }
}
