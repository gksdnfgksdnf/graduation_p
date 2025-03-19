using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixingZone : MonoBehaviour
{
    [SerializeField]
    private ItemType baseType; 
    [SerializeField]
    private Item baseItem;

    private List<ToolType> toolType; //useable Item

    private void Awake()
    {
        toolType = new List<ToolType> { ToolType.Jigger, ToolType.MixingGlass, ToolType.Shaker };
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

        // 나간 아이템이 현재 베이스 아이템이라면 제거
        if (item != null && item == baseItem)
        {
            Debug.Log($"기본 아이템 제거: {baseItem.gameObject.name}");
            baseItem = null;  // 기본 아이템 제거
        }
    }

    private Item GetItem(Collider2D other)
    {
        return other.transform.GetComponent<Item>();  // 드래그 가능한 아이템 반환
    }

    private void HandleAdditionalItem(Item item)
    {
        // 기본 아이템이 설정되어 있을 때, 새로운 아이템을 추가하는 처리
        // 아이템 간 상호작용 (예시로 Use() 함수 호출)
        item.Use();
    }

    private bool IsValidItemType(Item item)
    {
        ItemType itemType = item.itemData.itemType;

        if (itemType != baseType)
            return false;

        if (itemType == ItemType.Tool)
        {
            ToolSO toolData = item.itemData as ToolSO;
            if (toolData != null && toolType.Contains(toolData.toolType))
                return true;

            Debug.Log(toolData.toolType + "은 올바른 도구가 아닙니다!");
        }


        return true;
    }


    private IEnumerator PlaceItem(Item item)
    {
        while (Vector3.Distance(item.transform.position, transform.position) > 0.01f)
        {
            // 매 프레임마다 이동
            item.transform.position = Vector3.Lerp(
                item.transform.position,
                transform.position,
                item.smoothSpeed * Time.deltaTime
            );
            yield return null;
        }

        item.transform.position = transform.position;
    }
}
