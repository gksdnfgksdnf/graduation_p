using UnityEngine;
using System.Collections.Generic;

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
        toolType = new List<ToolType> { ToolType.Jigger,
                                        ToolType.MixingGlass,
                                        ToolType.Shaker };
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Item"))
        {
            baseItem = other.transform.GetComponent<DraggableItem>();
            if (baseItem.item.itemType != baseType)
                return;

            if (baseItem.item.itemType == ItemType.Tool)
            {
                ToolSO toolData = baseItem.item as ToolSO;
                if (toolData != null && toolType.Contains(toolData.type))
                {
                    //Jigger, MixingGlass, Shaker
                    PlaceItem();
                }
            }
            else if (baseItem.item.itemType == ItemType.Glass)
            {
                //Glass
                PlaceItem();
            }


        }



    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            baseItem = collision.transform.GetComponent<DraggableItem>();

            addedItems.Remove(baseItem);
        }
    }

    private void PlaceItem()
    {
        baseItem.transform.position = transform.position; //나중에 트윈 걸 예정

        addedItems.Add(baseItem);
    }
}
