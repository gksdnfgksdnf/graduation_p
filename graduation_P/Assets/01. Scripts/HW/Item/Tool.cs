using System;
using UnityEngine;

public class Tool : Item, IIngredientContainer
{
    [HideInInspector]
    public ToolSO toolData;
    private ToolType _toolType;

    private void Awake()
    {
        toolData = itemData as ToolSO;

        Initialize(ItemType.Tool);
    }

    public override void Use()
    {
        Debug.Log("도구사용!!");
    }


    public bool IsContainer()
    {
        return toolData.isContainer;
    }
    public void AddIngredient(Ingredient ingredient)
    {
        Debug.Log("도구에 재료 추가");

    }

    public void UseTool(Tool tool)
    {
        Debug.Log("도구에 도구 사용");

    }
}
