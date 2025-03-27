using UnityEngine;

public class MixingZone : BaseZone
{
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
                Debug.Log($"이 아이템은 {baseType} 타입이 아니거나 올바르지 않은 도구입니다.");
            }
        }
        else
        {
            HandleAdditionalItem(item);
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
        ItemType baseItemType = item.itemType;

        Debug.Log(baseItemType);

        switch (baseItemType)
        {
            case ItemType.Tool:
                cocktailMaker.ProcessUseTool(baseItem, item as Tool);
                break;
            case ItemType.Ingredient:
                cocktailMaker.ProcessAddIngredient(baseItem, item as Ingredient);
                break;
            default:
                Debug.Log("유효하지 않은 아이템 타입" + baseItemType);
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
            return toolData.isContainer;

        return true;
    }

}
