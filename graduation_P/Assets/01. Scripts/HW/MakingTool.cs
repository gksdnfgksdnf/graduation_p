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

[System.Serializable]
public class MakingTool
{
    public Tool_Type type;
}
