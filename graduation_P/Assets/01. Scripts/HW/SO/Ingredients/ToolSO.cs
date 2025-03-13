using UnityEngine;

public enum Tool_Type
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

    public Tool_Type type;
}
