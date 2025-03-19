using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixingZone : MonoBehaviour
{
    private CocktailMaker cocktailMaker;

    [SerializeField]
    private ItemType baseType; 
    [SerializeField]
    private Item baseItem;

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

    private Item GetItem(Collider2D other)
    {
        return other.transform.GetComponent<Item>();
    }

    private void HandleAdditionalItem(Item item)
    {
        ItemType baseItemType = item.itemData.itemType;
        if (baseItemType == ItemType.Tool)
        {
            cocktailMaker.UseTool(item as Tool);
        }
        else if(baseItemType == ItemType.Ingredient)
        {

            cocktailMaker.AddIngredient(item as Ingredient);
        }
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
