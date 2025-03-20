using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DecisionButton : MonoBehaviour
{
    public Vector2 textPadding = new Vector2(30, 30);

    private RectTransform rectTrm;
    private Button button;
    private TextMeshProUGUI textBase;

    private Decision decision;
    private Action<Decision> callback;

    public void Init()
    {
        rectTrm = transform as RectTransform;
        button = GetComponent<Button>();
        textBase = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Active(Decision decision, Action<Decision> callback)
    {
        gameObject.SetActive(true);
        this.decision = decision;
        this.callback = callback;
        textBase.text = decision.text;
        rectTrm.sizeDelta = textBase.GetPreferredValues() + textPadding;
        rectTrm.sizeDelta = textBase.GetPreferredValues() + textPadding;
        button.onClick.AddListener(HandleButtonClick);
    }

    public void Inactive()
    {
        button.onClick.RemoveListener(HandleButtonClick);
        textBase.text = "";
        callback = null;
        decision = null;
        gameObject.SetActive(false);
    }

    private void HandleButtonClick()
    {
        callback?.Invoke(decision);
    }
}
