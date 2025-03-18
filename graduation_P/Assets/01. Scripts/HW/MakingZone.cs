using System.Collections.Generic;
using UnityEngine;

public class MixingZone : MonoBehaviour
{
    [SerializeField]
    private ItemType baseType;

    [SerializeField]
    private List<DraggableItem> addedItems;

    private DraggableItem baseItem;

    private List<ToolType> toolType;

    private void Awake()
    {
        toolType = new List<ToolType> { ToolType.Jigger, ToolType.MixingGlass, ToolType.Shaker };
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (baseItem == null)
        {
            baseItem = other.transform.GetComponent<DraggableItem>();
            Debug.Log(baseItem.gameObject.name);

            if (!IsValidItemType(baseItem))
            {
                Debug.Log(baseItem.item.itemType + "는 " + baseType + "와/과 일치S지 않습니다.");
                return;
            }

            if (CanPlaceItem(baseItem))
            {
                PlaceItem();
            }
        }
        else
        {

            //ingredient, another tool
            DraggableItem item = other.transform.GetComponent<DraggableItem>();

            item.Use();
        }
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
            return toolData != null && toolType.Contains(toolData.type);
        }
        else if (item.item.itemType == ItemType.Glass)
        {
            return true;
        }
        return false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            DraggableItem item = collision.transform.GetComponent<DraggableItem>();
            if (addedItems.Contains(item))
                addedItems.Remove(item);
            if (baseItem == item)
                baseItem = null;
        }
    }

    private void PlaceItem()
    {
        baseItem.transform.position = transform.position; // 나중에 트윈 추가 예정
        addedItems.Add(baseItem);
    }
}
