using System;
using System.Collections.Generic;
using UnityEngine;

public class MixingZone : MonoBehaviour
{
    [SerializeField]
    private ItemType baseType;
    [SerializeField]
    private DraggableItem baseItem;

    private List<ToolType> toolType;

    private void Awake()
    {
        toolType = new List<ToolType> { ToolType.Jigger, ToolType.MixingGlass, ToolType.Shaker };
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DraggableItem item = GetDraggableItem(other);

        // 아이템이 없거나 유효하지 않은 타입이면 무시
        if (item == null || !IsValidItemType(item))
            return;

        // 기본 아이템이 비어있으면 설정
        if (baseItem == null)
        {
            baseItem = item;
            PlaceItem(baseItem);
            Debug.Log($"기본 아이템 설정: {baseItem.gameObject.name}");
        }
        else
        {
            // 기존 아이템이 있을 때 새로운 아이템과 상호작용 처리
            HandleAdditionalItem(item);
            Debug.Log($"추가 아이템 처리: {item.gameObject.name}");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        DraggableItem item = GetDraggableItem(collision);

        // 나간 아이템이 현재 베이스 아이템이라면 제거
        if (item != null && item == baseItem)
        {
            Debug.Log($"기본 아이템 제거: {baseItem.gameObject.name}");
            baseItem = null;
        }
    }

    private DraggableItem GetDraggableItem(Collider2D other)
    {
        return other.transform.GetComponent<DraggableItem>();
    }

    private void HandleAdditionalItem(DraggableItem item)
    {
        if (baseItem != null && baseItem != item)
        {
            Debug.Log($"기존 아이템과 새로운 아이템 상호작용: {baseItem.gameObject.name} + {item.gameObject.name}");
            // 기존 아이템과 상호작용 처리
            baseItem.Use();  // 예시: 기존 아이템이 상호작용하는 로직
        }

        // 새로운 아이템 사용
        item.Use();
    }

    private bool IsValidItemType(DraggableItem item)
    {
        return item.item.itemType == baseType;
    }

    private void PlaceItem(DraggableItem item)
    {
        item.transform.position = transform.position; // 나중에 트윈 추가 예정
    }

    private void OnMouseDown()
    {
        Debug.Log("Mixing zone Click");
    }
}
