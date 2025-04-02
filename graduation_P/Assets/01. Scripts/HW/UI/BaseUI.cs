using UnityEngine;
using UnityEngine.UIElements;

public abstract class BaseUI : MonoBehaviour
{
    [SerializeField]
    public VisualTreeAsset visualTreeAsset;

    protected UIDocument _uiDocument;

    protected VisualElement root;

    protected virtual void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
        root = _uiDocument.rootVisualElement;
    }

    public abstract void Close();
    public abstract void Open();
}
