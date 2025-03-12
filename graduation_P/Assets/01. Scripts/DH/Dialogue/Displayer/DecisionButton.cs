using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DecisionButton : MonoBehaviour
{
    public Vector2 textPadding = new Vector2(30, 30);

    private RectTransform rectTrm;
    private Button button;
    private TextMeshProUGUI textBase;

    private DialogueDecision decision;
    private DecisionDisplayer displayer;

    public void Init()
    {
        rectTrm = transform as RectTransform;
        button = GetComponent<Button>();
        textBase = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Active(DialogueDecision decision, DecisionDisplayer displayer)
    {
        gameObject.SetActive(true);
        this.decision = decision;
        this.displayer = displayer;
        textBase.text = decision.text;
        rectTrm.sizeDelta = textBase.GetPreferredValues() + textPadding;
        button.onClick.AddListener(HandleButtonClick);
    }

    public void Inactive()
    {
        button.onClick.RemoveListener(HandleButtonClick);
        textBase.text = "";
        displayer = null;
        decision = null;
        gameObject.SetActive(false);
    }

    private void HandleButtonClick()
    {
        displayer.Select(decision);
    }
}
