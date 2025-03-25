using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class DecisionManager : MonoBehaviour
{
    public static DecisionManager Instance { get; private set; }

    public DecisionDisplayer displayer;
    [SerializeField] private CanvasGroup group;

    private void Awake()
    {
        Instance = this;
        displayer.Init();
        Enable(false);
    }

    public void Enable(bool enable)
    {
        group.blocksRaycasts = enable;
        group.interactable = enable;
        group.alpha = enable ? 1 : 0;
    }
}
