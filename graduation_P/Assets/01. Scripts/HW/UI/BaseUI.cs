using UnityEngine;
using UnityEngine.UIElements;

public abstract class BaseUI : MonoBehaviour
{
    [SerializeField]
    protected VisualTreeAsset visualTreeAsset;

    protected UIDocument _uiDocument;


    protected virtual void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
    }    


}
