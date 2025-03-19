using System;
using System.Collections.Generic;
using UnityEngine;

public class MixingZone : MonoBehaviour
{
    [SerializeField]
    private ItemType baseType;

    private DraggableItem baseItem;

    private List<ToolType> toolType;

    private void Awake()
    {
        toolType = new List<ToolType> { ToolType.Jigger, ToolType.MixingGlass, ToolType.Shaker };
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DraggableItem item = GetDraggableItem(other);
        if (baseItem == null)
        {
            baseItem = item;
            if (!IsValidItemType(baseItem))
                return;

            if (CanPlaceItem(baseItem))
            {
                PlaceItem();
            }
        }
        else
        {
            //ingredient, another tool

            HandleAdditionalItem(item);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        DraggableItem item = GetDraggableItem(collision);
        if (baseItem == item)
            baseItem = null;
    }

    private DraggableItem GetDraggableItem(Collider2D other)
    {
        return other.transform.GetComponent<DraggableItem>();
    }

    private void HandleAdditionalItem(DraggableItem item)
    {
        Debug.Log("dmd");
    }

    private bool IsValidItemType(DraggableItem item)
    {
        return item.item.itemType == baseType;
    }

    private bool CanPlaceItem(DraggableItem item)
    {
        if (item.item.itemType == ItemType.Tool)
        {
            ToolSO toolData = item.item as ToolSO;
            return (toolData != null && toolType.Contains(toolData.type));
        }
        else if (item.item.itemType == ItemType.Glass)
            return true;

        return false;
    }
    private void PlaceItem()
    {
        baseItem.transform.position = transform.position; // 나중에 트윈 추가 예정
    }

}
