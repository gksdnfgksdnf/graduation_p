using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MixingZone : BaseZone
{
    private CocktailMaker cocktailMaker;

    private List<ToolType> toolType; //useable Item

    private void Awake()
    {
        toolType = new List<ToolType> { ToolType.Jigger, ToolType.MixingGlass, ToolType.Shaker };
    }

    private void Start()
    {
        cocktailMaker = FindFirstObjectByType<CocktailMaker>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Item item = GetItem(other);

        if (item == null)
            return;

        if (baseItem == null)
        {
            if (IsValidItemType(item))
            {
                baseItem = item;
                StartCoroutine(PlaceItem(item));
                Debug.Log($"기본 아이템 설정: {baseItem.gameObject.name}");
            }
            else
            {
                Debug.Log($"이 아이템은 {baseType} 타입이어야 합니다. {item.itemData.itemType}");
            }
        }
        else
        {
            HandleAdditionalItem(item);
            Debug.Log($"추가 아이템 처리: {item.gameObject.name}");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Item item = GetItem(collision);

        if (item != null && item == baseItem)
        {
            Debug.Log($"기본 아이템 제거: {baseItem.gameObject.name}");
            baseItem = null;
        }
    }

    private void HandleAdditionalItem(Item item)
    {
        ItemType baseItemType = item.itemData.itemType;

        switch (baseItemType)
        {
            case ItemType.Tool:
                cocktailMaker.UseTool(item as Tool);
                break;
            case ItemType.Ingredient:
                cocktailMaker.AddIngredient(item as Ingredient);
                break;
            case ItemType.Glass:
                cocktailMaker.UseGlass(item as Glass);
                break;
            default:
                break;

        }
    }

    protected override bool IsValidItemType(Item item)
    {
        // 기본 아이템 타입
        if (!base.IsValidItemType(item))
            return false;

        // ToolSO 일때
        if (item.itemData is ToolSO toolData)
        {
            // 가능한 도구인지
            return toolType.Contains(toolData.toolType);
        }

        return true;
    }

}
