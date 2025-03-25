using NUnit.Framework;
using System.Collections.Generic;
using System.Diagnostics;
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
    public bool isContainer;
    public List<ToolSO> requireTools;

    [HideInInspector]
    public List<Ingredient> ingredients;

}
