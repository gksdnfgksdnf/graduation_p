using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class RecpieItem : VisualElement
{
    private Label nameLabel;
    private VisualElement icon;

    public RecpieItem()
    {
        // UXML에서 불러올 때 호출됨
        var root = this;
        nameLabel = root.Q<Label>("name-label");
        icon = root.Q<VisualElement>("sprite");
    }

    public void Bind(CocktailDataSO data)
    {

    }
}
