using UnityEngine;

public enum ToolType
{
    Shaker,
    Jigger,
    MixingGlass,
    BarSpoon,
    Squezzer,
    Strainer,
    Muddler,
    Pourer,
    CocktailPin
}

[CreateAssetMenu(fileName = "NewTool", menuName = "SO/Item/Tool")]
public class ToolSO : ItemSO
{
    public ToolType toolType;
}
